window.onload = () => {
    $("#DataForm").slideUp();
}
let ShowEditForm = (ItemId) => {
    $("#DataForm").slideDown();
    fetch("/Admin/GetCategoryData?Id=" + ItemId)
        .then(res => {
            res.text().then(data => {
                document.getElementById("CartegoryId").value = ItemId;
                document.getElementById("Category_Title").value = data;
            })
        })
}
let ShowAddForm = () => {
    $("#DataForm").slideToggle();
}