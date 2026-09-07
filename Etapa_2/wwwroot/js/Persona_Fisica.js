$(function () {

    iniciarDataTable();
    iniciarSelect2();

    let telefonos = [];
    let correos = [];

    $("#btnNuevaPersona").on("click", function () {
        $("#Id").val("");
        limpiarFormulario();
        $("#modalPersona").modal("show");
   
        $("#modalPersona").modal("show");
        
    });

    $("#btnGuardar").on("click", guardarPersona);

    $(document).on("click", ".btnEditar", editarPersona);
    $(document).on("click", ".btnEliminar", eliminarPersona);

});

$("#btnAgregarTelefono").on(click(function () {

    let numero = prompt("Ingrese el teléfono");

    if (!numero)
        return;

    telefonos.push({
        id: 0,
        numero: numero
    });

    cargarTelefonos();
}));

$("#btnAgregarCorreo").on (click(function () {

    let correo = prompt("Ingrese el correo");

    if (!correo)
        return;

    correos.push({
        id: 0,
        correo: correo
    });

    cargarCorreos();
}));


function iniciarDataTable() {

    $("#tablaPersonas").DataTable({
        responsive: true,
        pageLength: 10
    });

}

function iniciarSelect2() {

    $(".select2").select2({
        width: "100%",
        dropdownParent: $("#modalPersona")
    });

}

function guardarPersona() {

    console.log("Id:", $("#Id").val());
    console.log("IdOriginal:", $("#IdOriginal").val());
    if ($("#Nombre").val().trim() === "") {

        Swal.fire({
            icon: "warning",
            title: "Validación",
            text: "Debe ingresar el nombre."
        });

        return;
    }

    if ($("#Apellido1").val().trim() === "") {

        Swal.fire({
            icon: "warning",
            title: "Validación",
            text: "Debe ingresar el primer apellido."
        });

        return;
    }

    if ($("#FechaNacimiento").val() === "") {

        Swal.fire({
            icon: "warning",
            title: "Validación",
            text: "Debe ingresar la fecha de nacimiento."
        });

        return;
    }

    let persona = {

        Cedula: $("#Id").val() || 0,
        IdOriginal: $("#IdOriginal").val(),
        Nombre: $("#Nombre").val(),
        Apellido1: $("#Apellido1").val(),
        Apellido2: $("#Apellido2").val(),

        FechaNacimiento: $("#FechaNacimiento").val(),

        Sexo: parseInt($("#Sexo").val()),
        Nacionalidad: parseInt($("#Nacionalidad").val()),
        Tipo: parseInt($("#TipoPersona").val()),

        Estado: $("#Estado").is(":checked")


    };
    console.log("Valor del input oculto:", $("#Id").val());
    console.log(persona);

    $.ajax({

        url: "/Usuario/Guardar",
        type: "POST",
        data: persona,

        success: function (response) {

            if (response.success) {

                Swal.fire({
                    icon: "success",
                    title: "Éxito",
                    text: response.message
                }).then(() => {

                    $("#modalPersona").modal("hide");

                    location.reload();

                });

            }
            else {

                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message
                });

            }

        },

        error: function (xhr, status, error) {

            console.error(xhr);
            console.error(status);
            console.error(error);

            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Ocurrió un error al guardar el registro."
            });

        }

    });

}

function editarPersona() {
    

    let id = $(this).data("id");

    $.ajax({
        url: "/Usuario/Obtener",
        type: "GET",
        data: { id: id },

       

        success: function (response) {

            if (response.success) {

                let p = response.data;
               
                $("#Id").val(p.cedula);
                
                $("#modalPersona").modal("show");
                $("#IdOriginal").val(p.id);
                $("#Nombre").val(p.nombre);
                $("#Apellido1").val(p.apellido1);
                $("#Apellido2").val(p.apellido2);

                $("#FechaNacimiento").val(
                    p.fechaNacimiento.split('T')[0]
                );

                $("#Sexo").val(p.sexo).trigger("change");
                $("#Nacionalidad").val(p.nacionalidad).trigger("change");
                $("#TipoPersona").val(p.tipo).trigger("change");

                $("#Estado").prop("checked", p.estado);

                $("#modalPersona").modal("show");
            }
            else {

                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message
                });
            }
        }
    });
}


function eliminarPersona() {

    let id = $(this).data("id");

    Swal.fire({
        title: "Eliminar registro",
        text: "¿Está seguro de eliminar esta persona?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {

        if (result.isConfirmed) {

            window.location.href = "/Usuario/Delete/" + id;

        }

    });

}

function cargarTelefonos() {

    let html = "";

    telefonos.forEach((t, index) => {

        html += `
            <tr>
                <td>${t.numero}</td>
                <td>
                    <button type="button"
                            class="btn btn-danger btn-sm"
                            onclick="eliminarTelefono(${index})">
                        Eliminar
                    </button>
                </td>
            </tr>`;
    });

    $("#tblTelefonos tbody").html(html);
}

function eliminarTelefono(index) {

    telefonos.splice(index, 1);

    cargarTelefonos();
}

function cargarCorreos() {

    let html = "";

    correos.forEach((c, index) => {

        html += `
            <tr>
                <td>${c.correo}</td>
                <td>
                    <button type="button"
                            class="btn btn-danger btn-sm"
                            onclick="eliminarCorreo(${index})">
                        Eliminar
                    </button>
                </td>
            </tr>`;
    });

    $("#tblCorreos tbody").html(html);
}

function eliminarCorreo(index) {

    correos.splice(index, 1);

    cargarCorreos();
}

function limpiarFormulario() {

    $("#Id").val("");

    $("#Nombre").val("");
    $("#Apellido1").val("");
    $("#Apellido2").val("");

    $("#FechaNacimiento").val("");

    $("#Sexo").val("").trigger("change");
    $("#Nacionalidad").val("").trigger("change");
    $("#TipoPersona").val("").trigger("change");

    $("#Estado").prop("checked", true);
}