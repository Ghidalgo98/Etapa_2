
// Variables globales
let telefonos = [];
let correos = [];

$(function () {
    iniciarDataTable();
    iniciarSelect2();

    // Botón Nueva Persona
    $("#btnNuevaPersona").on("click", function () {
        $("#Id").val("");
        limpiarFormulario();

        // Reiniciar listas
        correos = [];
        telefonos = [];
        cargarCorreos();
        cargarTelefonos();

        // Reiniciar Ids ocultos
        $("#Id").val("");          // Cedula
        $("#IdOriginal").val("");  // Id interno

        $("#modalPersona").modal("show");
    });

    // Guardar persona
    $("#btnGuardar").on("click", guardarPersona);

    // Editar / Eliminar persona
    $(document).on("click", ".btnEditar", editarPersona);
    $(document).on("click", ".btnEliminar", function () {
        let id = $(this).data("id"); // 👈 aquí obtienes el número
        eliminarPersona(id);         // 👈 ahora sí pasas el número

    });
});

// --- Teléfonos ---
$("#btnAgregarTelefono").on("click", function () {
    let numero = prompt("Ingrese el teléfono");
    if (!numero) return;

    telefonos.push({ id: 0, numero: numero });
    cargarTelefonos();
});

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

// --- Correos ---
$("#btnAgregarCorreo").on("click", function () {
    let correo = prompt("Ingrese el correo");
    if (!correo) return;

    correos.push({ id: 0, correo: correo });
    cargarCorreos();
});

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

// --- DataTable & Select2 ---
function iniciarDataTable() {
    $("#tablaPersonas").DataTable({
        responsive: true,
        pageLength: 10,
        ajax: {
            url: "/Usuario/Listar",   // 👈 tu acción que devuelve JSON
            type: "GET",
            dataSrc: "data"
        },
        columns: [
            { data: "id" },
            { data: "cedula" },
            { data: "nombreCompleto" },
            { data: "fechaNacimiento" },
            { data: "sexo" },
            { data: "nacionalidad" },
            { data: "estado" },
            { data: "acciones" }
        ]
    });
}


function iniciarSelect2() {
    $(".select2").select2({
        width: "100%",
        dropdownParent: $("#modalPersona")
    });
}

// --- Guardar Persona ---
function guardarPersona() {
    if ($("#Nombre").val().trim() === "") {
        Swal.fire({ icon: "warning", title: "Validación", text: "Debe ingresar el nombre." });
        return;
    }
    if ($("#Apellido1").val().trim() === "") {
        Swal.fire({ icon: "warning", title: "Validación", text: "Debe ingresar el primer apellido." });
        return;
    }
    if ($("#FechaNacimiento").val() === "") {
        Swal.fire({ icon: "warning", title: "Validación", text: "Debe ingresar la fecha de nacimiento." });
        return;
    }

    let persona = {
        Cedula: $("#Id").val() || 0,
        IdOriginal: $("#IdOriginal").val() || 0,
        Nombre: $("#Nombre").val(),
        Apellido1: $("#Apellido1").val(),
        Apellido2: $("#Apellido2").val(),
        FechaNacimiento: $("#FechaNacimiento").val(),
        Sexo: parseInt($("#Sexo").val()),
        Nacionalidad: parseInt($("#Nacionalidad").val()),
        Tipo: parseInt($("#TipoPersona").val()),
        Estado: $("#Estado").is(":checked"),
        Correos: correos.map(c => c.correo),
        Telefonos: telefonos.map(t => t.numero)
    };

    $.ajax({
        url: "/Usuario/Guardar",
        type: "POST",
        data: persona,
        success: function (response) {
            if (response.success) {
                Swal.fire({ icon: "success", title: "Éxito", text: response.message })
                    .then(() => {
                        $("#modalPersona").modal("hide");
                        cargarCorreos();
                        cargarTelefonos();
                        $("#tablaPersonas").DataTable().ajax.reload(); // si usas DataTable con ajax
                    });
            } else {
                Swal.fire({ icon: "error", title: "Error", text: response.message });
            }
        },
        error: function () {
            Swal.fire({ icon: "error", title: "Error", text: "Ocurrió un error al guardar el registro." });
        }
    });
}

// --- Editar Persona ---
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
                $("#IdOriginal").val(p.id);
                $("#Nombre").val(p.nombre);
                $("#Apellido1").val(p.apellido1);
                $("#Apellido2").val(p.apellido2);
                $("#FechaNacimiento").val(p.fechaNacimiento.split('T')[0]);
                $("#Sexo").val(p.sexo).trigger("change");
                $("#Nacionalidad").val(p.nacionalidad).trigger("change");
                $("#TipoPersona").val(p.tipo).trigger("change");
                $("#Estado").prop("checked", p.estado);

                // Correos con validación
                correos = (p.correos && p.correos.length > 0)
                    ? p.correos.map(c => ({ id: c.correoIdCorreo, correo: c.descripcionCorreoPersona }))
                    : [];
                cargarCorreos();

                // Teléfonos con validación
                telefonos = (p.telefonos && p.telefonos.length > 0)
                    ? p.telefonos.map(t => ({ id: t.telefonoIdTelefono, numero: t.numeroTelefonoPersona }))
                    : [];
                cargarTelefonos();

                $("#modalPersona").modal("show");
            } else {
                Swal.fire({ icon: "error", title: "Error", text: response.message });
            }
        },
        error: function (xhr, status, error) {
            Swal.fire({ icon: "error", title: "Error", text: "No se pudo consultar la persona: " + error });
        }
    });
}

// --- Eliminar Persona ---
function eliminarPersona(id) {
    Swal.fire({
        title: "¿Está seguro?",
        text: "Esta acción eliminará la persona seleccionada.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Usuario/Eliminar",
                type: "POST",
               
                data: { id: id },//JSON SIMPLE: form-unlencoled
                success: function (response) {
                    if (response.success) {
                        Swal.fire({ icon: "success", title: "Éxito", text: response.message });
                        $("#tablaPersonas").DataTable().ajax.reload();
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "No se puede eliminar",
                            text: response.message
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        icon: "error",
                        title: "Error",
                        text: "Ocurrió un error inesperado al intentar eliminar."
                    });
                }
            });
        }
    });
}


// --- Limpiar Formulario ---
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
