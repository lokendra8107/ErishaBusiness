$(document).ready(function () {
    $('.saveBannerLayoutbtn').on('click', function (e) {
        e.stopPropagation();
    });
    $('.inputfieldValidate').keydown(function () {
        if ($(this).val() == '') {
            $(this).addClass('reuiredValidateInput');
            $(this).parent('.inputfieldValidateItem').find('.reuiredValidateMessage').show()
        }
        else {
            $(this).removeClass('reuiredValidateInput');
            $(this).parent('.inputfieldValidateItem').find('.reuiredValidateMessage').hide();
        }
    });
});

function SaveBannerLayoutData() {
    var url = $("#BannerLayoutForm").attr("action");
    var isFormValid = true;
    if ($('#Title').val() == '' || $('#LayoutType').val() == '' || $('#CategoryId').val() == '') {
        isFormValid = false;
        var rowIndex = 0;
        $(".inputfieldValidate").each(function () {
            if ($(this).val() == '') {
                $(this).addClass('reuiredValidateInput');
                $('.reuiredValidateMessage:eq(' + rowIndex +')').show();
            }
            else {
                $(this).removeClass('reuiredValidateInput');
                $('.reuiredValidateMessage:eq(' + rowIndex +')').hide();
            }
            rowIndex++;
        });
    }
    if (isFormValid == true) {
        var BannerLayoutData = {
            Title: $('#Title').val(),
            LayoutType: $('#LayoutType').val(),
            CategoryId: $('#CategoryId').val(),
            isActive: $('#IsActive').is(":checked") ? true : false,
            id: $('#Id').val()
        };
        var formData = new FormData();
        formData.append("data", JSON.stringify(BannerLayoutData));
        formData.append("BannerLayoutImageFile", $("#ImageUpload")[0].files[0]);
        $('.overlay').show();
        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
        }).done(function (response) {
            $('.overlay').hide();
            if (response.status === "success") {
                ShowSuccessAlert(response.message, '/admin/bannerlayout');
            }
            else {
                ShowErrorAlert(response.message);
            }
        });
    }
}

function ShowPreview(input) {
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $('#BannerLayoutPreviewImage').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}  