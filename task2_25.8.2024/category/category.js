


async function GetCategories() {
  const url = 'https://localhost:44363/api/Categories/Categorys/getAllCategories';
    
var response = await fetch(url);
var result = await response.json();

var container = document.getElementById('container');

result.forEach(element => {
    
   
container.innerHTML += `<div class="card">
     <h5 class="card-title">${element.categoryName}</h5>
     <img src="${element.categoryImage}" class="card-img-top" style="width:300px;height:200px ;" alt="...">
     <div class="card-body">
       <h5 class="card-title">${element.categoryId}</h5>
       <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
       <a href="#" class="btn btn-primary">Go somewhere</a>
       <button onclick="ShowDetails(${element.categoryId})" class="btn btn-danger">Show More</button>
     </div>
   </div> `;


});

}
GetCategories();

async function ShowDetails(categoryId) {
    localStorage.setItem('categoryId', categoryId);
    alert('Category saved in local storage');
    window.location.href ="/product/product.html";
 
}



