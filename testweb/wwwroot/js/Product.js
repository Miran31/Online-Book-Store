

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "26%" },
            { data: 'isbn', "width": "16%" },
            { data: 'listPrice', "width": "11%" },
            { data: 'author', "width": "16%" },
            { data: 'category.name', "width": "11%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-57 btn-group" role="group">
                        <a href="/admin/product/update?id=${data}" class="btn btn-primary rounded-3 mx-2">
                            <i class="bi bi-pencil-square"></i> Edit 
                        </a>
                        <a href="/admin/product/delete/${data}" class="btn btn-danger rounded-3">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": "20%"
            }
        ]
    });
}

