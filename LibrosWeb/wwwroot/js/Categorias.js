

var dataTable;

$(document).ready(function () {
    CargarDataTable();
});

function CargarDataTable() {
  dataTable= $("#tblCategorias").DataTable({
        "ajax": {
          "url": "/Categorias/GetTodasCategorias",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "nombre", "width": "40%" },
            { "data": "fechaCreacion", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">Editar</a>
                              &nbsp;
                            <a onclick = Delete("/Categorias/Edit/${data}") class="btn btn-danger text-white" style="cursor:pointer">Borrar</a>


                        </div>

                    `

                }
            }


        ]
    })
}

function Delete(url) {
    swal({
        title: "Esta segoro de eliminar el registro",
        text: "Este recurso no se podra recuperar",
        icon: "warning",
        button: true,
        //buttonMode: true,
        dangerMode: true

    }).ajax({
        type:"DELETE",
        url: url,
        success(data) {
            if (data.success){
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);

            }
        }
    });
}