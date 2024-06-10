const tblBlog = "blogs";
let blogId = null;

getBlogTable();

//readBlog();
//createBlog();
//updateBlog("302b53f5-de15-4482-aee7-e6c3f1b9cf5a",'pp','pp','pp');
deleteBlog("6beee976-f298-4570-b281-dc5fc25dea6f");

function readBlog(){
    let lst = getBlogs();
    console.log(blogs);
}

function editBlog(id){
    let lst = getBlogs();
   
    const items = lst.filter(x => x.id === id);
    console.log(items);

    console.log(items.length);

    if(items.length == 0){
        console.log("No data found");
        errorMessage("No data found.");
        return;
    }

    //return items[0];
    let item = items[0];
    blogId = item.id;

    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();
}

function createBlog(title,author,content){

    let lst = getBlogs();

    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(requestModel);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonBlog);

    successMessage("Saving Successful.");
    clearControls();
}

function updateBlog(id,title,author,content){
    let lst = getBlogs();
   
    const items = lst.filter(x => x.id === id);
    console.log(items);

    console.log(items.length);

    if(items.length == 0){
        console.log("No data found");
        errorMessage("No data found.");
        return;
    }

    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonBlog);

    successMessage('Updating Succesful.');
}

function deleteBlog(id){
    let result = confirm("Are you sure want to delete?");
    if(!result) return;

    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    if(items.length == 0){
        console.log("No data found");
        return;
    }

    lst = lst.filter(x => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog,jsonBlog);

    successMessage("Deleting Successful.");
    getBlogTable();
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
      (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );

}

function getBlogs(){
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }

    return lst;
}

$('#btnSave').click(function(){
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if(blogId === null){
        createBlog(title,author,content);
    }
    else{
        updateBlog(blogId,title,author,content);
        blogId =  null;
    }

    getBlogTable();
})

function successMessage(message){
    alert(message);
}

function errorMessage(message){
    alert(message);
}

function clearControls(){
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogTable(){
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>
        `;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);
}