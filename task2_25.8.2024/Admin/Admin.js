
const url = 'https://localhost:44363/api/Categories/Categorys/getAllCategories';

async function GetCategories() {
    
var response = await fetch(url);
var result = await response.json();

var container = document.getElementById('tablecategory');

result.forEach(element => {
    
   
container.innerHTML +=
 `<tr>
    <td>${element.categoryId}</td>
    <td>${element.categoryName}</td>
    <td><img src="${element.categoryImage}" class="card-img-top" style="width:100px;height:50px ;" alt="..."></td>
    <td><button onclick="updatecategory(${element.categoryId})" class="btn btn-danger">Edit</button></td>
   </tr> `;
});

}
GetCategories();

async function updatecategory(categoryId) {
    localStorage.setItem('categoryId', categoryId);
    alert('Category saved in local storage');
    window.location.href ="/Admin/Updatecategory.html";
 
}
