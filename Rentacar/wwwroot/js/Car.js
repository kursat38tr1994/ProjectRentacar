var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/car/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "licensePlate", "width": "20%" },
            { "data": "currentPrice", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/car/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:150px;'>
                                    <i class='far fa-edit'></i> Updaten
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/car/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:150px;'>
                                    <i class='far fa-trash-alt'></i> Verwijderen
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "Geen record kunnen vinden!"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Weet u het zeker dat u het wilt verwijderen?",
        text: "U kunt het niet meer terug halen!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Ja, Verwijder",
        cancelButtonText: "Nee, Niet verwijderen",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}