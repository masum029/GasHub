$(document).ready(async function () {
    await getLocalStorageList();
    updateTotals();
    addressOptions();
});

async function getLocalStorageList() {
    debugger
    var storedProducts = JSON.parse(localStorage.getItem('productIds')) || {};
    var products = await getProduct();
    var productMap = {};

    products.forEach(function (product) {
        productMap[product.id] = product;
    });

    var $productList = $('#product-list');
    $productList.empty(); // Clear the current list
    const productDiscuns = await $.ajax({
        url: '/ProductDiscun/GetallProductDiscun',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8'
    });
    var productDiscunMap = {};
    productDiscuns.data.forEach(function (discount) {
        productDiscunMap[discount.productId] = discount;
    });
    Object.keys(storedProducts).forEach(function (id) {
        var product = productMap[id];
        var quantity = storedProducts[id];
        var discount = productDiscunMap[id];
        var discounted = discount ? discount.discountedPrice : 0; // Check if discountedPrice exists
        debugger
        if (product) {
            var productHtml = `
            <div class="d-flex justify-content-between mb-2" data-product-id="${id}">
                <div class="d-flex gap-2">
                    <div class="rounded-pil">
            
                        <img src="/images/${product?.prodImage}" alt="product-img" style="width: 60px;">
                    </div>
                    <div class="d-flex align-items-start flex-column">
                        <h5>${product.name}</h5>
                        <div class="d-flex mt-auto">
                            <button class="btn decrement-btn"> - </button>
                            <p class="btn quantity">${quantity}</p>
                            <button class="btn increment-btn"> + </button>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="d-flex align-items-start flex-column">
                        <h5 class="fs-4"> Tk : <span>${(product.prodPrice - discounted) * quantity}</span> </h5>
                        <a class="remove-btn">Remove</a>
                    </div>
                </div>
            </div>`;
        }
        

        $productList.append(productHtml);
    });

    updateTotals();
}

async function getProduct() {
    try {
        const response = await $.ajax({
            url: '/Product/GetAllProduct',
            type: 'get',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8'
        });

        return response.data || [];
    } catch (error) {
        console.log('Error:', error);
        return [];
    }
}

async function updateTotals() {
    var storedProducts = JSON.parse(localStorage.getItem('productIds')) || {};
    var products = await getProduct();
    var productMap = {};
    var subtotal = 0;
    var total = 0;
    var totalDiscount = 0;
    const productDiscuns = await $.ajax({
        url: '/ProductDiscun/GetallProductDiscun',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8'
    });
    var productDiscunMap = {};
    productDiscuns.data.forEach(function (discount) {
        productDiscunMap[discount.productId] = discount;
    });
    // Create a product map for easy access
    products.forEach(function (product) {
        productMap[product.id] = product;
    });

    // Calculate the subtotal
    Object.keys(storedProducts).forEach(function (id) {
        var product = productMap[id];
        debugger
        var discount = productDiscunMap[id];
        if (discount) {
            subtotal += (product.prodPrice - discount.discountedPrice )* storedProducts[id]; // Assuming the product price is stored in `product.price`
            totalDiscount += discount.discountedPrice * storedProducts[id];
        } else {
            subtotal += product.prodPrice * storedProducts[id];
        }
        total = subtotal;
    });

    // Update the HTML
    $('#subtotal').text(subtotal);
    $('#discount').text(totalDiscount);
    $('#total').text(total);
}

// Ensure to call updateTotals() when necessary, e.g., after adding/removing/updating products in the cart


$(document).on('click', '.decrement-btn', async function () {
    debugger
    var $productDiv = $(this).closest('[data-product-id]');
    var productId = $productDiv.data('product-id');
    var storedProducts = JSON.parse(localStorage.getItem('productIds')) || {};

    if (storedProducts[productId] > 1) {
        storedProducts[productId]--;
    } else {
        delete storedProducts[productId];
        $productDiv.remove();
    }

    localStorage.setItem('productIds', JSON.stringify(storedProducts));
    await getLocalStorageList();
    updateTotals();
});

$(document).on('click', '.increment-btn', function () {
    var $productDiv = $(this).closest('[data-product-id]');
    var productId = $productDiv.data('product-id');
    var storedProducts = JSON.parse(localStorage.getItem('productIds')) || {};

    storedProducts[productId]++;
    localStorage.setItem('productIds', JSON.stringify(storedProducts));
    getLocalStorageList();
});

$(document).on('click', '.remove-btn', function () {
    debugger
    var $productDiv = $(this).closest('[data-product-id]');
    var productId = $productDiv.data('product-id');
    var storedProducts = JSON.parse(localStorage.getItem('productIds')) || {};

    // Show SweetAlert confirmation dialog
    Swal.fire({
        title: 'Are you sure?',
        text: "Do you really want to remove this product?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, remove it!',
        cancelButtonText: 'No, keep it'
    }).then((result) => {
        if (result.isConfirmed) {
            // User confirmed, proceed with removal
            delete storedProducts[productId];
            localStorage.setItem('productIds', JSON.stringify(storedProducts));
            $productDiv.remove();
            updateTotals();

            // Optionally, show success message
            Swal.fire(
                'Removed!',
                'The product has been removed.',
                'success'
            );
        }
    });
});

async function addressOptions() {
    try {
        debugger
        const userId = $('#address-container').data('user-id');


        const deliveryAddressList = await getAddressList();
        const loginUser = await GetLoginUserById(userId);
        const deliveryAddress = deliveryAddressList.find(x => x.userId === userId);
        console.log(loginUser);
        if (deliveryAddress) {
            const { mobile, address } = deliveryAddress;

            if (mobile && address) {
                $('#address-container').html(`
                        
                       <p>Delivery Address </p>
                        <div class="d-flex w-100 flex-column p-2" id="address-details" style="background-color: #dcdcdc;">
                            <span id="Name">${loginUser?.firstName} ${loginUser?.lastName}</span>
                            <span id="phonNumber">${mobile}</span>
                            <span id="addressDetails" class="w-100">${address}</span>
                        </div>
                `);
            } else if (mobile) {
                $('#address-container').html(`
                    <p>Delivery Address </p>
                        <div class="d-flex w-100 flex-column p-2" id="address-details" style="background-color: #dcdcdc;">
                            <span id="Name">${loginUser?.firstName} ${loginUser?.lastName}</span>
                            <span id="phonNumber">${mobile}</span>
                            <span id="addressDetails" class="w-50">${address}</span>
                        </div>
                `);
            } else {
                $('#address-container').html(`
                     <div class="d-flex w-100 flex-column justify-content-center" id="address-details">
                        <p class="mx-auto">Please add your address before order</p>
                        <button  id="add-address-btn" class="btn btn-sm w-50 btn-dark mx-auto mt-2 text-white">Add Address</button>
                    </div>
                `);
            }
        } else {
            $('#address-container').html(`
                 <div class="d-flex w-100 flex-column justify-content-center" id="address-details">
                        <p class="mx-auto">Please add your address before order</p>
                        <button  id="add-address-btn" class="btn btn-sm w-50 btn-dark mx-auto mt-2 text-white">Add Address</button>
                    </div>
            `);
        }
    } catch (error) {
        console.error('Error fetching address details:', error);
    }
}
$(document).on('click', '#add-address-btn', function (e) {
    e.preventDefault(); // Prevent the default form submission
    window.location.href = "/Home/AddAddress";
});


async function getAddressList() {
    debugger
    try {
        const response = await $.ajax({
            url: '/DeliveryAddress/GetDeliveryAddressList',
            type: 'get',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8'
        });

        return response.data || [];
    } catch (error) {
        console.log('Error:', error);
        return [];
    }
}
async function GetLoginUserById(id) {
    debugger
    try {
        const response = await $.ajax({
            url: '/User/GetById/' + id,
            type: 'get',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8'
        });

        return response || [];
    } catch (error) {
        console.log('Error:', error);
        return [];
    }
}
