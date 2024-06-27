// utility.js

// Generic AJAX call function
export async function ajaxApiCall(url, type, data = {}, dataType = 'json', contentType = 'application/json;charset=utf-8') {
    try {
        const response = await $.ajax({
            url: url,
            type: type,
            data: data,
            dataType: dataType,
            contentType: contentType
        });
        return response;
    } catch (error) {
        console.log('AJAX Error:', error);
        throw error;
    }
}

// Generic function to initialize DataTable
export function initializeDataTable(selector, data, columns) {
    if ($.fn.DataTable.isDataTable(selector)) {
        // If initialized, destroy the DataTable first
        $(selector).DataTable().destroy();
    }
    $(selector).dataTable({
        processing: true,
        lengthChange: true,
        lengthMenu: [[5, 10, 20, 30, -1], [5, 10, 20, 30, 'All']],
        searching: true,
        ordering: true,
        paging: true,
        data: data,
        columns: columns
    });
}

// Generic form validation initialization
export function initializeFormValidation(formSelector, rules, messages) {
    return $(formSelector).validate({
        onkeyup: function (element) {
            $(element).valid();
        },
        rules: rules,
        messages: messages,
        errorElement: 'div',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
}
