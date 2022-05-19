function readURL(input) {
    if (input.files && input.files[0]) {
  
      var reader = new FileReader();
  
      reader.onload = function(e) {
        $('.image-upload-wrap').hide();
  
        $('.file-upload-image').attr('src', e.target.result);
        $('.file-upload-content').show();
  
        $('.image-title').html(input.files[0].name);
      };
  
      reader.readAsDataURL(input.files[0]);
  
    } else {
      removeUpload();
    }
  }
  
  function removeUpload() {
    $('.file-upload-input').replaceWith($('.file-upload-input').clone());
    $('.file-upload-content').hide();
    $('.image-upload-wrap').show();
  }
  $('.image-upload-wrap').bind('dragover', function () {
      $('.image-upload-wrap').addClass('image-dropping');
    });
    $('.image-upload-wrap').bind('dragleave', function () {
      $('.image-upload-wrap').removeClass('image-dropping');
    });

let ChangeProductTitle = (element) => {
    if (element.value != "" && element.value != null) {
        document.getElementById("ProductTitle").innerText = element.value;
    }
}
let ChangeProductPrice = (element) => {
    if (element.value != "" && element.value != null) {
        document.getElementById("ProductPrice").innerHTML = element.value + ' <i class="fas fa-dollar-sign"></i>';
    }
}
let ChangeProductDiscription = (element) => {
    if (element.value != "" && element.value != null) {
        document.getElementById("ProductDescription").innerText = element.value;
    }
}
let SubmitData = () => {
    var data = new FormData();
  
    let ProductPrice = document.getElementById("ProductPrice").innerText;
    let ProductDescription = document.getElementById("ProductDescription").innerText;
    let ProductCategory = document.getElementById("ProductCategory").value;
    let ProductOldTitle = document.getElementById("OldProductTitle").value;
    
    data.append('Image', $('input[type=file]')[0].files[0]);

    data.append('ProductPrice', ProductPrice);
    data.append('ProductDescription', ProductDescription);
    data.append('ProductCategory', ProductCategory);
    data.append('UserName', document.getElementById("UserName").value);
    data.append('ProductOldTitle', ProductOldTitle);

    fetch('/User/EditProductPost', {
        method: 'POST',
        body: data
    }).then(res => {
        window.location.reload();
    })
}