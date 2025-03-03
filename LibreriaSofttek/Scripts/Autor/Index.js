$(document).ready(function () {
    $(".btn-delete").on("click", function (e) {
        e.preventDefault();

        let id = $(this).data("id");

        Swal.fire({
            title: "¡Atención!",
            text: "¿Está seguro de eliminar el autor? Esta acción no se puede deshacer.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar",
            customClass: {
                confirmButton: "btn btn-outline-danger ms-2",
                cancelButton: "btn btn-outline-secondary ms-2"
            },
            buttonsStyling: false
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = "/Autor/Delete/" + id;
            }
        });
    });
});
