var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/GetOrder",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "user.firstname", "width": "15%" },
            { "data": "user.phoneNumber", "width": "15%" },
            { "data": "user.email", "width": "15%" },
            { "data": "total", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Order/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> Factuur Bekijken
                                </a>
                            </div>
                           `;
                }, "width": "5%"
            }
        ],
        "language": {
            "emptyTable":"Geen record kunnen vinden!"
        },
        "width":"100%"
    });
}
