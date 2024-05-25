$(document).ready(async function () {
    await GetCompanyList();
    await GetProductSizeList();
    await GetProductList1();

    let selectedCompanyId = null;
    let selectedSizes = [];

    // Event listener for company filter
    // Add event listener for company filter
    $('#category-list').on('click', 'a', function (e) {
        e.preventDefault();
        debugger
        const selectedCompanyId = $(this).data('company-id');
        GetProductList(selectedCompanyId, selectedSizes);
    });

    // Event listener for size filter
    $('#size-filter').on('change', 'input[name="size-filter"]', function () {
        debugger;
        selectedSizes = $('input[name="size-filter"]:checked').map(function () {
            return $(this).attr('id').split('=')[1];
        }).get();
        console.log('Selected sizes:', selectedSizes);  // Debugging statement
        GetProductList(selectedCompanyId, selectedSizes);
    });
});

async function GetCompanyList() {
    debugger
    try {
        const company = await $.ajax({
            url: '/Company/GetCompanyList',
            type: 'get',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8'
        });

        if (company && company.data && company.data.length > 0) {

            var categoryList = $('#category-list');
            categoryList.empty();

            $.each(company.data, function (index, category) {
                var listItem = $('<li></li>');
                var link = $('<a></a>')
                    .attr('href', '#')
                    .attr('data-company-id', category.id)
                    .text(category.name);

                listItem.append(link);
                categoryList.append(listItem);
            });
        }
    } catch (error) {
        console.log('Error fetching categories:', error);
    }
}

async function GetProductSizeList() {
    try {
        const prodSize = await $.ajax({
            url: '/ProductSize/GetallProductSize',
            type: 'get',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8'
        });

        if (prodSize && prodSize.data && prodSize.data.length > 0) {
            var sizeFilter = $('#size-filter');
            sizeFilter.empty();

            $.each(prodSize.data, function (index, size) {
                var filterDiv = $('<div></div>').addClass('input-save d-flex align-items-center');
                var checkbox = $('<input>')
                    .attr('type', 'checkbox')
                    .addClass('form-check-input')
                    .attr('name', 'size-filter')
                    .attr('id', 'size=' + size.id);
                var label = $('<label></label>')
                    .attr('for', 'size-' + size.id)
                    .text(size.size + " " + "Kg");

                filterDiv.append(checkbox).append(label);
                sizeFilter.append(filterDiv);
            });
        }
    } catch (error) {
        console.log('Error fetching sizes:', error);
    }
}



async function GetProductList(companyId = null, sizeIds = []) {
    debugger
    try {
        const products = await $.ajax({
            url: '/Product/GetAllProduct',
            type: 'get',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8'
        });

        if (products && products.data) {
            var productSection = $('#product-section');
            productSection.empty();

            let filteredProducts = products.data;

            if (companyId) {
                filteredProducts = filteredProducts.filter(product => product.companyId === companyId);
            }

            if (sizeIds.length > 0) {
                filteredProducts = filteredProducts.filter(product => sizeIds.includes(product.prodSizeId.toString()));
            }

            $.each(filteredProducts, function (index, product) {
                var colDiv = $('<div></div>').addClass('col-xl-3 col-lg-3 col-md-3');
                var cardDiv = $('<div></div>').addClass('catagory-product-card-2 shadow-style text-center');
                var imgDiv = $('<div></div>').addClass('catagory-product-image');

                var img = '<img src="/images/' + product.prodImage + '" alt="Image" style="width: 100%;" />';

                var contentDiv = $('<div></div>').addClass('catagory-product-content');
                var buttonDiv = $('<div></div>').addClass('catagory-button');
                var button = $('<a></a>')
                    .attr('href', 'shop-cart.html')
                    .addClass('theme-btn-2')
                    .html('<i class="far fa-shopping-basket"></i>Order Now');
                var priceDiv = $('<div></div>').addClass('info-price d-flex align-items-center justify-content-center');

                var discountText = product.discount ? '-' + product.discount + '%' : '00';
                var originalPriceText = product.originalPrice ? '$' + product.originalPrice : '00';
                var discountedPriceText = product.discountedPrice ? '$' + product.discountedPrice : '00';

                var discount = $('<p></p>').text(discountText);
                var originalPrice = $('<h6></h6>').text(originalPriceText);
                var discountedPrice = $('<span></span>').text(discountedPriceText);

                var title = $('<h4></h4>').append($('<a></a>').attr('href', 'shop-single.html').text(product.name));
                var starDiv = $('<div></div>').addClass('star');

                for (var i = 0; i < 5; i++) {
                    var star = $('<span></span>').addClass('fas fa-star');
                    if (i >= product.rating) {
                        star.addClass('color-bg');
                    }
                    starDiv.append(star);
                }

                buttonDiv.append(button);
                priceDiv.append(discount).append(originalPrice).append(discountedPrice);
                contentDiv.append(buttonDiv).append(priceDiv).append(title).append(starDiv);
                cardDiv.append(imgDiv.append(img)).append(contentDiv);
                colDiv.append(cardDiv);
                productSection.append(colDiv);
            });
        }
    } catch (error) {
        console.log('Error fetching products:', error);
    }
}
async function GetProductList1() {
    debugger
    try {
        const products = await $.ajax({
            url: '/Product/GetAllProduct',
            type: 'get',
            dataType: 'json',
            contentType: 'application/json;charset=utf-8'
        });

        if (products && products.data) { 
            var productSection = $('#product-section');
            productSection.empty(); // Clear any existing product cards

            $.each(products.data, function (index, product) {
                // Create a new product card for each product
                var colDiv = $('<div></div>').addClass('col-xl-3 col-lg-3 col-md-3');
                var cardDiv = $('<div></div>').addClass('catagory-product-card-2 shadow-style text-center');
                var imgDiv = $('<div></div>').addClass('catagory-product-image');
                
                // Create an image element and set its attributes
                //var img = $('<img>').attr('src', '/images/' + product.prodImage).attr('alt', 'product-img');
                var img = '<img src="/images/' + product.prodImage + '" alt="Image" style="width: 100%;" />';

                var contentDiv = $('<div></div>').addClass('catagory-product-content');
                var buttonDiv = $('<div></div>').addClass('catagory-button');
                var button = $('<a></a>')
                    .attr('href', 'shop-cart.html')
                    .addClass('theme-btn-2')
                    .html('<i class="far fa-shopping-basket"></i>Order Now');
                var priceDiv = $('<div></div>').addClass('info-price d-flex align-items-center justify-content-center');

                // Check for discount, original price, and discounted price
                var discountText = product.discount ? '-' + product.discount + '%' : '00';
                var originalPriceText = product.originalPrice ? '$' + product.originalPrice : '00';
                var discountedPriceText = product.discountedPrice ? '$' + product.discountedPrice : '00';

                var discount = $('<p></p>').text(discountText);
                var originalPrice = $('<h6></h6>').text(originalPriceText);
                var discountedPrice = $('<span></span>').text(discountedPriceText);

                var title = $('<h4></h4>').append($('<a></a>').attr('href', 'shop-single.html').text(product.name));
                var starDiv = $('<div></div>').addClass('star');

                // Add star ratings
                for (var i = 0; i < 5; i++) {
                    var star = $('<span></span>').addClass('fas fa-star');
                    if (i >= product.rating) {
                        star.addClass('color-bg');
                    }
                    starDiv.append(star);
                }

                buttonDiv.append(button);
                priceDiv.append(discount).append(originalPrice).append(discountedPrice);
                contentDiv.append(buttonDiv).append(priceDiv).append(title).append(starDiv);
                cardDiv.append(imgDiv.append(img)).append(contentDiv);
                colDiv.append(cardDiv);
                productSection.append(colDiv);
            });
        }
    } catch (error) {
        console.log('Error fetching products:', error);

    }
}


