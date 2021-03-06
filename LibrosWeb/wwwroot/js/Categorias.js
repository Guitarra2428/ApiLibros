var dataTable;
$(document).ready(function () {

    loadDataTable();

});

function loadDataTable() {
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/Categorias/GetTodasCategorias",
            "type": "GET",
            "datatype": "json"
        },
        "columns":[
            { "data": "categoriaID", "width": "20%" },
            { "data": "nombre", "width": "40%" },
            { "data": "fechaCreacion", "width": "20%" },

            {
                "data": "categoriaID",
                "render": function (data) {
                    return `
                         <div class="text-center">
                              <a href="/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">Editar</a>
                                     &nbsp;
                             <a onclick = Delete("/Categorias/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">Borrar</a>

                        </div>

                      `;
                },"width":"20%"
            }

        ]
    });
}

function Delete(url) {
    swal({
        title: "Estas Seguro de eliminar el registro",
        text: "esta accion no puede se revertida",
        icon: "warning",
        buttons: true,
        dangerMode: true

    }).then((wilDelete) =>{
        if (wilDelete) {
            $.ajax({
                type: "DELETE",
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


        }
    });
}