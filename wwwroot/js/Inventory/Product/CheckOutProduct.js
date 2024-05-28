$(document).ready(async function () {
    await getLocalStorageList();
    updateTotals();
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

    Object.keys(storedProducts).forEach(function (id) {
        var product = productMap[id];
        var quantity = storedProducts[id];
        debugger
        var productHtml = `
            <div class="d-flex justify-content-between mb-2" data-product-id="${id}">
                <div class="d-flex gap-2">
                    <div class="rounded-pil">
            
                        <img src="/images/${product.prodImage}" alt="product-img" style="width: 60px;">
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
                        <h5 class="fs-4"> Tk : <span>${product.prodPrice * quantity}</span> </h5>
                        <a class="remove-btn">Remove</a>
                    </div>
                </div>
            </div>`;

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

    // Create a product map for easy access
    products.forEach(function (product) {
        productMap[product.id] = product;
    });
 
    // Calculate the subtotal
    Object.keys(storedProducts).forEach(function (id) {
        var product = productMap[id];
        if (product) {
            subtotal += product.prodPrice * storedProducts[id]; // Assuming the product price is stored in `product.price`
        }
    });

    // Update the HTML
    $('#subtotal').text(subtotal);
    $('#total').text(subtotal);
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
