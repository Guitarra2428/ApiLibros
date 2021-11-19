var dataTable;
$(document).ready(function () {

    loadDataTable();

});

function loadDataTable() {
    dataTable = $("#tbLibros").DataTable({
        "ajax": {
            "url": "/Libros/GetTodasLibros",
            "type": "GET",
            "datatype": "json"
        },
        "columns":[
            { "data": "libroID", "width": "20%" },
            { "data": "titulo", "width": "20%" },
            { "data": "descripcion", "width": "20%" },
            { "data": "fechaLanzamiento", "width": "20%" },
            { "data": "categoriaID", "width": "20%" },
         

            {
                "data": "libroID",
                "render": function (data) {
                    return `
                         <div class="text-center">
                              <a href="/Libros/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">Editar</a>
                                     &nbsp;
                             <a onclick = Delete("/Libros/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">Borrar</a>

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