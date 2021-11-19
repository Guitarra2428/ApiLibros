var dataTable;
$(document).ready(function () {

    loadDataTable();

});

function loadDataTable() {
    dataTable = $("#tbAutors").DataTable({
        "ajax": {
            "url": "/Autors/GetTodasAutors",
            "type": "GET",
            "datatype": "json"
        },
        "columns":[
            { "data": "autorId", "width": "20%" },
            { "data": "nombre", "width": "20%" },
            { "data": "apellido", "width": "20%" },
            { "data": "sexo", "width": "20%" },
            { "data": "edad", "width": "20%" },
            { "data": "libroID", "width": "40%" },
            { "data": "fechaNacimiento", "width": "20%" },

            {
                "data": "autorId",
                "render": function (data) {
                    return `
                         <div class="text-center">
                              <a href="/Autors/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">Editar</a>
                                     &nbsp;
                             <a onclick = Delete("/Autors/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">Borrar</a>

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