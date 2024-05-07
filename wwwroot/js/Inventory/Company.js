$(document).ready(function () {
    GetCompanyList();
});

function GetCompanyList() {
    $.ajax({
        url: '/Company/GetCompanyList',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            if (data && data.data && data.data.length > 0) {
                const companies = data.data;
                onSuccess(companies)
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error:', errorThrown);
        }
    });
}

function onSuccess(companies) {
    if (companies.length > 0) {
        if ($.fn.DataTable.isDataTable('#CompanyTable')) {
            // If initialized, destroy the DataTable first
            $('#CompanyTable').DataTable().destroy();
        }
        $('#CompanyTable').dataTable({
            processing: true,
            lengthChange: true,
            lengthMenu: [[5, 10, 20, 30, -1], [5, 10, 20, 30, 'All']],
            searching: true,
            ordering: true,
            paging: true,
            data: companies,
            columns: [
                {
                    data: 'Name',
                    render: function (data, type, row, meta) {
                        return row.name;
                    }
                },
                {
                    data: 'Contactperson',
                    render: function (data, type, row, meta) {
                        return row.contactperson;
                    }
                },
                {
                    data: 'ContactPerNum',
                    render: function (data, type, row, meta) {
                        return row.contactPerNum;
                    }
                },
                {
                    data: 'ContactNumber',
                    render: function (data, type, row, meta) {
                        return row.contactNumber;
                    }
                },
                {
                    data: 'IsActive',
                    render: function (data, type, row, meta) {
                        return row.isActive ? '<button class="btn btn-sm btn-primary rounded-pill">Yes</button>' : '<button class="btn btn-sm btn-danger rounded-pill">No</button>';
                    }
                },
                {
                    data: 'BIN',
                    render: function (data, type, row, meta) {
                        return row.bin;
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


$('#btn-Create').click(function () {
    $('#modelCreate input[type="text"]').val('');
    $('#modelCreate').modal('show');
    $('#btnSave').show();
    $('#btnUpdate').hide();
});

$('#btnSave').click(function () {
    var formData = $('#CompanyForm').serialize(); // Serialize the form data

    $.ajax({
        url: '/Company/Create',
        type: 'post',
        contentType: 'application/x-www-form-urlencoded', // Correct content type
        data: formData, // Send serialized form data
        success: function (response) {
            console.log('success:', response);
            $('#modelCreate').modal('hide');
            if (response === true) {
                // Show success message
                $('#successMessage').text('Your company was successfully saved.');
                $('#successMessage').show();
                GetCompanyList();
                $('#CompanyForm')[0].reset();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error:', errorThrown);
        }
    });
});

// Edit Company
function editCompany(id) {
    console.log("Edit company with id:", id);
    $.ajax({
        url: '/Company/GetCompany/' + id,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {
            // Populate form fields with company data
            $('#btnSave').hide();
            $('#btnUpdate').show();
            $('#CompanyForm').attr('action', '/Company1/Update/' + id);
            $('#Name').val(data.name);
            $('#Contactperson').val(data.contactperson);
            $('#ContactPerNum').val(data.contactPerNum);
            $('#ContactNumber').val(data.contactNumber);
            $('#CreatedBy').val(data.createdBy);
            $('#UpdatedBy').val(data.updatedBy);
            $('#IsActive').val(data.isActive ? 'true' : 'false');
            $('#DeactivatedDate').val(data.deactivatedDate);
            $('#DeactiveBy').val(data.deactiveBy);
            $('#BIN').val(data.bin);

            // Show modal for editing
            $('#modelCreate').modal('show');

            // Update button click event handler
            $('#btnUpdate').click(function () {
                updateCompany(id);
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error:', errorThrown);
        }
    });
}

function updateCompany(id) {
    const formData = $('#CompanyForm').serialize();

    console.log(formData);
    $.ajax({
        url: '/Company/Update/' + id,
        type: 'post',
        contentType: 'application/x-www-form-urlencoded',
        data: formData,
        success: function (response) {
            $('#modelCreate').modal('hide');
            if (response === true) {
                // Show success message
                $('#successMessage').text('Your company was successfully updated.');
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
            url: '/Company/Delete',
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

// Function to populate company details in the modal
function populateCompanyDetails(company) {
    $('#companyDetails').empty();

    var details = [
        { label: 'Name', value: company.name },
        { label: 'Contact Person', value: company.contactperson },
        { label: 'Contact Person Num', value: company.contactPerNum },
        { label: 'Contact Number', value: company.contactNumber },
        { label: 'Active', value: company.isActive ? 'Yes' : 'No' },
        { label: 'Deactivated Date', value: company.deactivatedDate },
        { label: 'Deactivated By', value: company.deactiveBy },
        { label: 'BIN', value: company.bin },
        { label: 'Creation Date', value: company.creationDate },
        { label: 'Update Date', value: company.updateDate },
        { label: 'Created By', value: company.createdBy },
        { label: 'Updated By', value: company.updatedBy }
    ];

    details.forEach(function (detail) {
        $('#companyDetails').append('<div class="row mb-2">' +
            '<div class="col-sm-12 col-md-2 col-form-label text-md-end fw-bold">' + detail.label + ':</div>' +
            '<div class="col-sm-12 col-md-10">' + detail.value + '</div>' +
            '</div>');
    });



}



