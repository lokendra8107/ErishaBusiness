$(document).ready(function () {
    $('.saveSiteSettingbtn').on('click', function (e) {
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

function SaveSiteSettingData() {
    var url = $("#SiteSettingForm").attr("action");
    var isFormValid = true;
    isFormValid = false;
    var rowIndex = 0;
    $(".inputfieldValidate").each(function () {
        if ($(this).val() == '') {
            $(this).addClass('reuiredValidateInput');
            $('.reuiredValidateMessage:eq(' + rowIndex + ')').show();
            isFormValid = false;
        }
        else {
            $(this).removeClass('reuiredValidateInput');
            $('.reuiredValidateMessage:eq(' + rowIndex + ')').hide();
            isFormValid = true;
        }
        rowIndex++;
    });
    if (isFormValid == true) {
        var SiteSettingData = {
            AdminEmail: $('#AdminEmail').val(),
            Phone: $('#Phone').val(),
            Address: $('#Address').val(),
            MailFromEmail: $('#MailFromEmail').val(),
            SmtpServer: $('#SmtpServer').val(),
            SmtpuserName: $('#SmtpuserName').val(),
            SmtpuserPassword: $('#SmtpuserPassword').val(),
            SmtpPort: $('#SmtpPort').val(),
            isActive: $('#IsActive').is(":checked") ? true : false,
            id: $('#Id').val()
        };
        var formData = new FormData();
        formData.append("data", JSON.stringify(SiteSettingData));
        formData.append("SiteSettingImageFile", $("#ImageUpload")[0].files[0]);
        formData.append("SiteSettingFeviconImageFile", $("#FaviconImageUpload")[0].files[0]);
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
                ShowSuccessAlert(response.message, '');
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
            $('#SiteSettingPreviewImage').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}  
function ShowFaviconPreview(input) {
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $('#SiteSettingFaviconPreviewImage').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
} 