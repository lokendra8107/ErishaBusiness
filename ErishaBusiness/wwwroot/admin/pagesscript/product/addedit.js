$(document).ready(function () {
    $('.saveProductbtn').on('click', function (e) {
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

    var sizeChartItem = $('#SizeChart').val().split(',');
    for (i = 0; i < sizeChartItem.length; i++) {
        $('#SizeChart_' + sizeChartItem[i]).prop("checked", true);
    }
});
var ImageItemData = [];
var descriptionValue;
ClassicEditor
    .create(document.querySelector('#editor'), {
        // toolbar: [ 'heading', '|', 'bold', 'italic', 'link' ]
    })
    .then(editor => {
        window.editor = editor;
        descriptionValue = editor;
    })
    .catch(err => {
        console.error(err.stack);
    });

function SaveProductData() {
    var url = $("#ProductForm").attr("action");
    var isFormValid = true;
    isFormValid = false;
    var rowIndex = 0;
    $(".inputfieldValidate").each(function () {
        if ($(this).val() == '') {
            $(this).addClass('reuiredValidateInput');
            $('.reuiredValidateMessage:eq(' + rowIndex + ')').show();
        }
        else {
            $(this).removeClass('reuiredValidateInput');
            $('.reuiredValidateMessage:eq(' + rowIndex + ')').hide();
        }
        rowIndex++;
    });
    isFormValid = SizeChartValidation(isFormValid);
    if (isFormValid == true) {
        var sizeChart = '';
        $('.sizechartValidation').find('input').each(function () {
            if ($(this).is(":checked")) {
                sizeChart += (sizeChart == '' ? $(this).attr('data-value') : ',' + $(this).attr('data-value'))
            }
        });

        var ProductData = {
            ProductName: $('#ProductName').val(),
            CategoryId: $('#CategoryId').val(),
            Description: descriptionValue.getData(),
            IsActive: $('#IsActive').is(":checked") ? true : false,
            SizeChart: sizeChart,
            MaterialName: $('#MaterialName').val(),
            NewArrival: $('#NewArrival').is(":checked") ? true : false,
            BestSelling: $('#BestSelling').is(":checked") ? true : false,
            TopBrand: $('#TopBrand').is(":checked") ? true : false,
            MetaTitle: $('#MetaTitle').val(),
            MetaDescription: $('#MetaDescription').val(),
            MetaKeyword: $('#MetaKeyword').val(),
            ProductColor: $('#ProductColor').val(),
            Quantity: $('#Quantity').val(),
            GSTPrice: $('#GSTPrice').val(),
            SKUId: $('#SKUId').val(),
            ProductBreadth: $('#ProductBreadth').val(),
            ProductHeight: $('#ProductHeight').val(),
            ProductWeight: $('#ProductWeight').val(),
            ProductComboSet: $('#ProductComboSet').val(),
            ProductPrice: $('#ProductPrice').val(),
            ProductPrice_S: $('#ProductPrice_S').val(),
            ProductPrice_M: $('#ProductPrice_M').val(),
            ProductPrice_L: $('#ProductPrice_L').val(),
            ProductPrice_XL: $('#ProductPrice_XL').val(),
            ProductPrice_XXL: $('#ProductPrice_XXL').val(),
            ProductPrice_Combo: $('#ProductPrice_Combo').val(),
            id: $('#Id').val()
        };
        var formData = new FormData();
        formData.append("data", JSON.stringify(ProductData));
        formData.append("ProductImageFile", $("#ImageUpload")[0].files[0]);
        for (var j = 0; j < ImageItemData.length; j++) {
            formData.append("OtherProductImageFile", ImageItemData[j].file);
        }
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
                ShowSuccessAlert(response.message, '/admin/product');
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
            $('#ProductPreviewImage').attr('src', e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }
}
function SizeChartValidation(isFormValid) {
    var sizeChartIndex = 1;
    var isCheckedItem = false;
    $('.sizechartValidation').find('input').each(function () {
        if ($(this).is(":checked"))
            isCheckedItem = true;
        if (sizeChartIndex == $('.sizechartValidation').find('input').length) {
            if (isCheckedItem) {
                isFormValid = true;
                $('.sizechartValidationMessage').hide();
                $('.sizechartValidation').removeClass('fieldValidate')
            }
            else {
                isFormValid = false;
                $('.sizechartValidationMessage').show();
                $('.sizechartValidation').addClass('fieldValidate')
            }
        }
        sizeChartIndex++;
    });
    return isFormValid;
}

function ShowMultiFilePreview(input) {
    var rowItemIndex = 0;
    for (var i = 0; i < input.files.length; i++) {
        if (input.files && input.files[i]) {
            var ImageDir = new FileReader();
            ImageDir.onload = function (e) {
                $('.productImageItem').append('<div class="col-md-3 tempimageItems_' + rowItemIndex + '"><img style="width: 200px; height: 220px; border: 5px solid #ede3e3; padding: 3px;margin:5px" src="' + e.target.result + '" /><i class="mdi mdi-close-circle text-primary imagecloseicon" onclick="DeleteTempProductImage(' + rowItemIndex + ')"></i></div>');
                rowItemIndex++;
            }
            ImageDir.readAsDataURL(input.files[i]);
        }
        ImageItemData.push({ file: input.files[i], fileIndex: i })
    }
}

async function DeleteProductImage(Id) {
    await ShowDeleteAlert();
    if (deleteStatus == true) {
        $('.overlay').show();
        $.get("/admin/Product/DeleteProductImageById?Id=" + Id, function (data) {
            $('.overlay').hide();
            $('.imageItems_' + Id).remove();
            ShowSuccessAlert(data.data, '');
        });
    }
}

function DeleteTempProductImage(Id) {
    $('.tempimageItems_' + Id).remove();
    ImageItemData = ImageItemData.filter(x => x.fileIndex != Id);
}