let url = "https://localhost:44363/api/Categories/AddCaegory";

function addnewcategory(){
    
    
    
   let data= document.getElementById("formdata");

    const formData = new FormData(form);
    async function addnewcategory(){
    event.preventDefault();

    let response = await fetch (url , {
        method: 'POST',
        body : data,
    } );
    
    console.log(data);
    }
   
}
addnewcategory();

