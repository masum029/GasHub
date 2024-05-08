﻿$(document).ready(function () {
    GetCompanyList();
});

function GetCompanyList() {
    $.ajax({
        url: '/DeliveryAddress/GetDeliveryAddressList',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            if (data && data.data && data.data.length > 0) {
                const companies = data.data;
                companies.forEach(function (company) {
                    console.log('company:', company);
                    $.ajax({
                        url: '/User/GetallUser/' + company.userId,
                        type: 'get',
                        dataType: 'json',
                        contentType: 'application/json;charset=utf-8'
                    }).then(function (userData) {
                        console.log(userData.data);
                        var users = userData.data;
                        onSuccess(companies, users)
                    }).catch(function (error) {
                        console.log('Error fetching user details:', error);
                    })
                });
                
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error:', errorThrown);
        }
    });
}



function onSuccess(companies, users) {
    if (companies.length > 0 && users.length > 0) {
        if ($.fn.DataTable.isDataTable('#CompanyTable')) {
            // If initialized, destroy the DataTable first
            $('#CompanyTable').DataTable().destroy();
        }

        // Convert users array to a map for easy lookup
        var usersMap = {};
        users.forEach(function (user) {
            usersMap[user.id] = user;
        });

        // Merge company and user data
        var mergedData = companies.map(function (company) {
            var user = usersMap[company.userId];
            console.log('onSuccess:', company);
            console.log('onSuccess:', user);
            if (user) {
                return {
                    id: company.id,
                    email: user.email,
                    fullName: user.firstName + ' ' + user.lastName,
                    phone: company.phone,
                    mobile: company.mobile,
                    address: company.address,
                    isActive: company.isActive
                };
            }
            return null; // Skip if user not found
        }).filter(Boolean); // Remove null entries
        console.log('onSuccess:', mergedData);
        $('#CompanyTable').dataTable({
            processing: true,
            lengthChange: true,
            lengthMenu: [[5, 10, 20, 30, -1], [5, 10, 20, 30, 'All']],
            searching: true,
            ordering: true,
            paging: true,
            data: mergedData,
            columns: [
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return row.fullName; 
                    }
                },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return row.email; 
                    }
                },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return row.phone;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return row.mobile;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return row.address;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return row.isActive ? '<button class="btn btn-sm btn-primary rounded-pill">Yes</button>' : '<button class="btn btn-sm btn-danger rounded-pill">No</button>';
                    }
                },
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return '<button class="btn btn-primary btn-sm ms-1" onclick="editCompany(\'' + row.id + '\')">Edit</button>' + ' ' +
                            '<button class="btn btn-info btn-sm ms-1" onclick="showDetails(\'' + row.id + '\')">Details</button>' + ' ' +
                            '<button class="btn btn-danger btn-sm ms-1" onclick="deleteCompany(\'' + row.id + '\')">Delete</button>';
                    }
                }
            ]
        });
    }
}


//======================================================================



// Initialize validation
const companyForm = $('#CompanyForm').validate({
    onkeyup: function (element) {
        $(element).valid();
    },
    rules: {
        UserId: {
            required: true,
        },
        Address: {
            required: true,
            minlength: 2,
            maxlength: 50
        },
        Phone: {
            required: true,
            digits: true,
            minlength: 11,
            maxlength: 11
        },
        Mobile: {
            required: true,
            digits: true,
            minlength: 11,
            maxlength: 11
        },
        BIN: {
            required: true
        }
    },
    messages: {
        UserId: {
            required: " User Name is required.",
        },
        Address: {
            required: "Address is required.",
            minlength: "Address must be between 2 and 50 characters.",
            maxlength: "Address must be between 2 and 50 characters."
        },
        Phone: {
            required: "Phone Number is required.",
            digits: "Phone Number must contain only digits.",
            minlength: "Phone Number must be exactly 11 digits.",
            maxlength: "Phone Number must be exactly 11 digits."
        },
        Mobile: {
            required: "Mobile is required.",
            digits: "Mobile must contain only digits.",
            minlength: "Mobile must be exactly 11 digits.",
            maxlength: "Mobile must be exactly 11 digits."
        },
    },
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

// Bind validation on change
$('#CompanyForm input[type="text"]').on('change', function () {
    companyForm.element($(this));
});
$('#CompanyForm input[type="text"]').on('focus', function () {
    companyForm.element($(this));
});
function resetValidation() {
    companyForm.resetForm(); // Reset validation
    $('.form-group .invalid-feedback').remove(); // Remove error messages
    $('#CompanyForm input').removeClass('is-invalid'); // Remove error styling
}


$('#btn-Create').click(function () {
    $('#modelCreate input[type="text"]').val('');
    $('#modelCreate').modal('show');
    $('#btnSave').show();
    $('#btnUpdate').hide();
    populateUserDropdown();
});



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


// Open modal and focus on the first input field
$('#modelCreate').on('shown.bs.modal', function () {
    $('#CompanyForm input:first').focus();
});

// Listen for Enter key press on input fields
$('#modelCreate').on('keypress', 'input', handleEnterKey);

//======================================================================
// Submit button click event
$('#btnSave').click(function () {
    console.log("Save");
    // Check if the form is valid
    if ($('#CompanyForm').valid()) {
        // Proceed with form submission
        var formData = $('#CompanyForm').serialize();
        console.log(formData);
        $.ajax({
            url: '/DeliveryAddress/Create',
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: formData,
            success: function (response) {
                $('#modelCreate').modal('hide');
                if (response === true) {
                    // Show success message
                    $('#successMessage').text('Your Delivery Address was successfully saved.');
                    $('#successMessage').show();
                    GetCompanyList();
                    $('#CompanyForm')[0].reset();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error:', errorThrown);
            }
        });
    }
});





function populateUserDropdown() {
    $.ajax({
        url: '/User/GetallUser',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            // Clear existing options
            $('#userDropdown').empty();
            // Add default option
            $('#userDropdown').append('<option value="">Select User</option>');
            // Add user options
            console.log(data.data);
            $.each(data.data, function (index, user) {
                $('#userDropdown').append('<option value="' + user.id + '">' + user.userName + '</option>');
            });
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
            // Handle error
        }
    });
}

// Call the function to populate the dropdown when the page loads


// Optionally, you can refresh the user list on some event, like a button click
$('#refreshButton').click(function () {
    populateUserDropdown();
});




















// Edit Company
function editCompany(id) {
    console.log("Edit company with id:", id);
    populateUserDropdown();
    // Reset form validation
    debugger

    $.ajax({
        url: '/DeliveryAddress/GetById/' + id,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            // Populate form fields with company data
            $('#btnSave').hide();
            $('#btnUpdate').show();
            $('#userDropdown').val(data.userId);
            $('#address').val(data.address);
            $('#phone').val(data.phone);
            $('#mobile').val(data.mobile);

            debugger
            resetValidation()
            // Show modal for editing
            $('#modelCreate').modal('show');
            // Update button click event handler
            $('#btnUpdate').off('click').on('click', function () {
                updateCompany(id);
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error:', errorThrown);
        }
    });
}


function updateCompany(id) {

    if ($('#CompanyForm').valid()) {
        const formData = $('#CompanyForm').serialize();
        console.log(formData);
        $.ajax({
            url: '/DeliveryAddress/Update/' + id,
            type: 'put',
            contentType: 'application/x-www-form-urlencoded',
            data: formData,
            success: function (response) {
                $('#modelCreate').modal('hide');
                if (response === true) {
                    // Show success message
                    $('#successMessage').text('Your Delivery Address was successfully updated.');
                    $('#successMessage').show();
                    // Reset the form
                    $('#CompanyForm')[0].reset();
                    // Update the company list
                    GetCompanyList();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error:', errorThrown);
                // Show error message
                $('#errorMessage').text('An error occurred while updating the company.');
                $('#errorMessage').show();
            }
        });
    }
}




// Details Company
function showDetails(id) {
    $('#deleteAndDetailsModel').modal('show');
    // Fetch company details and populate modal
    $.ajax({
        url: '/Company/GetCompany', // Assuming this is the endpoint to fetch company details
        type: 'GET',
        data: { id: id },
        success: function (response) {
            console.log(response);
            // Assuming response contains company details
            populateCompanyDetails(response);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function deleteCompany(id) {
    $('#deleteAndDetailsModel').modal('show');

    $('#companyDetails').empty();
    $('#btnDelete').click(function () {
        $.ajax({
            url: '/DeliveryAddress/Delete',
            type: 'POST',
            data: { id: id },
            success: function (response) {
                $('#deleteAndDetailsModel').modal('hide');
                GetCompanyList();
            },
            error: function (xhr, status, error) {
                console.log(error);
                $('#deleteAndDetailsModel').modal('hide');
            }
        });
    });
}