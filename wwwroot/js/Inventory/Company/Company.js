import { ajaxApiCall, initializeDataTable, initializeFormValidation } from "../../utilitys/util.js";

$(document).ready(async function () {
    // As soon as the document is fully loaded and ready, execute the GetCompanyList function
    try {
        await GetCompanyList();  // Await the completion of the asynchronous GetCompanyList function
        console.log("Company list successfully retrieved.");  // Log success message
    } catch (error) {
        console.error("An error occurred while retrieving the company list:", error);  // Log error if something goes wrong
    }
});

// get all company lest 
async function GetCompanyList() {
    debugger
    try {
        const companys = await ajaxApiCall('/Company/GetCompanyList', 'get');
        if (companys && companys.data) {
            onSuccess(companys.data);
        }
    } catch (error) {
        console.log('Error:', error);
    }
}

function onSuccess(companies) {
    const columns = [
        { data: 'Name', render: (data, type, row) => row.name },
        { data: 'Contactperson', render: (data, type, row) => row.contactperson },
        { data: 'ContactPerNum', render: (data, type, row) => row.contactPerNum },
        { data: 'ContactNumber', render: (data, type, row) => row.contactNumber },
        { data: 'IsActive', render: (data, type, row) => row.isActive ? '<button class="btn btn-sm btn-primary rounded-pill">Yes</button>' : '<button class="btn btn-sm btn-danger rounded-pill">No</button>' },
        { data: 'BIN', render: (data, type, row) => row.bin },
        { data: null, render: (data, type, row) => `<button class="btn btn-primary btn-sm ms-1" onclick="editCompany('${row.id}')">Edit</button> <button class="btn btn-info btn-sm ms-1" onclick="showDetails('${row.id}')">Details</button> <button class="btn btn-danger btn-sm ms-1" onclick="deleteCompany('${row.id}')">Delete</button>` }
    ];
    initializeDataTable('#CompanyTable', companies, columns);
}

// Initialize validation
const companyForm = initializeFormValidation('#CompanyForm', {
    Name: { required: true, minlength: 2, maxlength: 50 },
    Contactperson: { required: true, minlength: 2, maxlength: 50 },
    ContactPerNum: { required: true, digits: true, minlength: 11, maxlength: 11 },
    ContactNumber: { required: true, digits: true, minlength: 11, maxlength: 11 },
    BIN: { required: true }
}, {
    Name: { required: "Name is required.", minlength: "Name must be between 2 and 50 characters.", maxlength: "Name must be between 2 and 50 characters." },
    Contactperson: { required: "Contact Person is required.", minlength: "Contact Person must be between 2 and 50 characters.", maxlength: "Contact Person must be between 2 and 50 characters." },
    ContactPerNum: { required: "Contact Person Number is required.", digits: "Contact Person Number must contain only digits.", minlength: "Contact Person Number must be exactly 11 digits.", maxlength: "Contact Person Number must be exactly 11 digits." },
    ContactNumber: { required: "Contact Number is required.", digits: "Contact Number must contain only digits.", minlength: "Contact Number must be exactly 11 digits.", maxlength: "Contact Number must be exactly 11 digits." },
    BIN: { required: "BIN is required." }
});

// Bind validation on change
$('#CompanyForm input[type="text"]').on('change focus', function () {
    companyForm.element($(this));
});
function resetValidation() {
    companyForm.resetForm(); // Reset validation
    $('.form-group .invalid-feedback').remove(); // Remove error messages
    $('#CompanyForm input').removeClass('is-invalid'); // Remove error styling
}






// Function to handle Enter key press
function handleEnterKey(event) {
    if (event.keyCode === 13) { // Check if Enter key is pressed
        event.preventDefault(); // Prevent default form submission
        if ($('#btnSave').is(":visible")) {
            $('#btnSave').click(); // Trigger save button click if Save button is visible
        } else if ($('#btnUpdate').is(":visible")) {
            $('#btnUpdate').click(); // Trigger update button click if Update button is visible
        }
    }
}




// Create btn 
$('#btn-Create').click(function () {
    $('#modelCreate input[type="text"]').val('');
    $('#modelCreate').modal('show');
    $('#btnSave').show();
    $('#btnUpdate').hide();
});

// Open modal and focus on the first input field
$('#modelCreate').on('shown.bs.modal', function () {
    $('#CompanyForm input:first').focus();
}).on('keypress', 'input', handleEnterKey);

// Submit button click event
$('#btnSave').click(async function () {
    if ($('#CompanyForm').valid()) {
        const formData = $('#CompanyForm').serialize();
        try {
            const response = await ajaxCall('/Company/Create', 'post', formData, 'json', 'application/x-www-form-urlencoded');
            if (response.success === true && response.status === 200) {
                $('#successMessage').text('Your company was successfully saved.').show();
                await GetCompanyList();
                $('#CompanyForm')[0].reset();
                $('#modelCreate').modal('hide');
            }
        } catch (error) {
            console.log('Error:', error);
        }
    }
});

// Edit Company
async function editCompany(id) {
    $('#myModalLabelUpdateEmployee').show();
    $('#myModalLabelAddEmployee').hide();
    try {
        const data = await ajaxCall(`/Company/GetCompany/${id}`, 'get');
        $('#btnSave').hide();
        $('#btnUpdate').show();
        $('#Name').val(data.name);
        $('#Contactperson').val(data.contactperson);
        $('#ContactPerNum').val(data.contactPerNum);
        $('#ContactNumber').val(data.contactNumber);
        $('#BIN').val(data.bin);
        resetValidation();
        $('#modelCreate').modal('show');
        $('#btnUpdate').off('click').on('click', function () {
            updateCompany(id);
        });
    } catch (error) {
        console.log('Error:', error);
    }
}


async function updateCompany(id) {
    if ($('#CompanyForm').valid()) {
        const formData = $('#CompanyForm').serialize();
        try {
            const response = await ajaxCall(`/Company/Update/${id}`, 'put', formData, 'json', 'application/x-www-form-urlencoded');
            if (response.success === true && response.status === 200) {
                $('#successMessage').text('Your company was successfully updated.').show();
                $('#CompanyForm')[0].reset();
                await GetCompanyList();
                $('#modelCreate').modal('hide');
            }
        } catch (error) {
            console.log('Error:', error);
            $('#errorMessage').text('An error occurred while updating the company.').show();
        }
    }
}

// Details Company
//async function showDetails(id) {
//    $('#deleteAndDetailsModel').modal('show');
//    try {
//        const response = await ajaxCall('/Company/GetCompany', 'GET', { id: id });
//        console.log(response);
//        populateCompanyDetails(response);
//    } catch (error) {
//        console.log(error);
//    }
//}

function deleteCompany(id) {
    $('#deleteAndDetailsModel').modal('show');
    $('#companyDetails').empty();
    $('#btnDelete').click(function () {
        ajaxCall('/Company/Delete', 'POST', { id: id })
            .then(response => {
                $('#deleteAndDetailsModel').modal('hide');
                $('#successMessage').text('Your company was successfully deleted.').show();
                GetCompanyList();
            })
            .catch(error => {
                console.log(error);
                $('#deleteAndDetailsModel').modal('hide');
            });
    });
}
