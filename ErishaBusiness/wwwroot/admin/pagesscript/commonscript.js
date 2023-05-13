var deleteStatus = false;
var successStatus = false;
async function ShowDeleteAlert() {
    await swal({
        title: "Delete",
        text: 'Are you sure you want to delete this record ?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value == true) {
            deleteStatus = true;
        }
        else
            deleteStatus = false;
    });
}

async function ShowSuccessAlert(msg, returnUrl) {
    await swal({
        title: "Success",
        text: msg,
        type: 'success',
        showCancelButton: false,
        confirmButtonText: "Ok"
    }).then((result) => {
        if (result.value == true) {
            if (returnUrl != '')
                window.location.href = returnUrl;
        }
    });
}
async function ShowErrorAlert(msg) {
    await swal({
        title: "Error",
        text: msg,
        type: 'error',
        showCancelButton: false,
        confirmButtonText: "Ok"
    });
}