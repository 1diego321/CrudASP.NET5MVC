window.addEventListener("load", function () {
    var alerMessage = document.querySelector("#btn-message-close");

    if (alerMessage != null) {
        alerMessage.addEventListener("click", function (e) {
            e.target.parentElement.remove();
        });
    }

    function bindTableEvents() {
        var btns = document.querySelectorAll("#btnBorrar");

        if (btns.length > 0) {
            btns.forEach(x => {
                x.addEventListener("click", function (e) {
                    let id = e.target.parentElement.querySelector("input[hidden]").value;

                    openDelete(id);
                });
            });
        }
    }

    function openDelete(id) {
        Swal.fire({
            title: 'Borrar Usuario',
            text: "Estás seguro de borrar la cuenta de usuario?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Borrar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch("ApplicationUser/Delete/" + id, {
                    method: 'DELETE',
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                })
                    .then(response => response.json())
                .then(data => {
                    if (data.success) refreshTable();
                    openSwalAlert(data.success, data.message);
                }).catch(function () {
                    openSwalAlert(false, "No se pudo establecer una conexión con el servidor, asegurate de que tengas conexión a internet.");
                });
            }
        });
    }

    function openSwalAlert(success, message) {
        Swal.fire({
            icon: (success === true) ? 'success' : 'error',
            title: (success === true) ? 'Exito!' : 'Error',
            text: message,
        });
    }

    function refreshTable() {
        $.get('ApplicationUser/RefreshTable', function (data) {
            $("#tblUsuarios").replaceWith(data);
            bindTableEvents()
        });
    }

    bindTableEvents();

}, false); 