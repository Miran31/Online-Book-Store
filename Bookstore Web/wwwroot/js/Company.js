﻿

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/Company/getall' },
        "columns": [
            { data: 'name', "width": "26%" },
            { data: 'streetAddress', "width": "16%" },
            { data: 'city', "width": "11%" },
            { data: 'state', "width": "16%" },
            { data: 'phoneNumber', "width": "11%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-57 btn-group" role="group">
                        <a href="/admin/company/update?id=${data}" class="btn btn-primary rounded-3 mx-2">
                            <i class="bi bi-pencil-square"></i> Edit 
                        </a>
                        <a onClick=Delete('/admin/company/delete/${data}') class="btn btn-danger rounded-3">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

