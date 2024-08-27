
let m = localStorage.getItem("categoryId");
let url = `https://localhost:44363/api/Categories/updateCategory/${m}`;

async function UpdateCategory() {
    event.preventDefault();
    let data = document.getElementById("formdataupdat");
    const formData = new FormData(data);
    var response = await fetch(url , {
    method: 'PUT',
    body: formData,
   });
    alert("Category updated successfully");
    // clear input fields
    document.getElementById("CategoryName").value = "";
    document.getElementById("CategoriesImage").value = "";
};

UpdateCategory();

