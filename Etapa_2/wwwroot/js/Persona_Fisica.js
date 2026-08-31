$(function () {

    iniciarDataTable();
    iniciarSelect2();

    $("#btnGuardar").on("click", guardarPersona);

    $(document).on("click", ".btnEliminar", eliminarPersona);

});

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

        Id: $("#Id").val() || 0,

        Nombre: $("#Nombre").val(),
        Apellido1: $("#Apellido1").val(),
        Apellido2: $("#Apellido2").val(),

        FechaNacimiento: $("#FechaNacimiento").val(),

        Sexo: parseInt($("#Sexo").val()),
        Nacionalidad: parseInt($("#Nacionalidad").val()),
        Tipo: parseInt($("#TipoPersona").val()),

        Estado: $("#Estado").is(":checked")

    };

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