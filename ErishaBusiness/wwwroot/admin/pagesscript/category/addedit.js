$(document).ready(function () {
    $('.saveCategorybtn').on('click', function (e) {
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

function SaveCategoryData() {
    var url = $("#CategoryForm").attr("action");
    var isFormValid = true;
    if ($('#CategoryName').val() == '' || $('#CategoryType').val() == '') {
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
        var categoryData = {
            categoryName: $('#CategoryName').val(),
            categoryType: $('#CategoryType').val(),
            isActive: $('#IsActive').is(":checked") ? true : false,
            id: $('#Id').val()
        };
        var formData = new FormData();
        formData.append("data", JSON.stringify(categoryData));
        formData.append("categoryImageFile", $("#ImageUpload")[0].files[0]);
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
                ShowSuccessAlert(response.message, '/admin/category');
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
            $('#categoryPreviewImage').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}  