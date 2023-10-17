
var uploadForm = document.querySelector('.addfile');
uploadForm.addEventListener('submit', function (event) {
    event.preventDefault(); 

    var formData = new FormData(uploadForm);
    formData.append('idtask', uploadForm.id);

    
    fetch('/Up/Upload', {
        method: 'POST',
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        var fileDiv = document.createElement('div');
fileDiv.className = 'file';

var img = document.createElement('img');
img.className = 'corner-img';
img.src = '/img/removefile.png';
img.id = data.newFileUpload.fileId;

fileDiv.appendChild(img);

var a = document.createElement('a');
a.href = '/Files/' + data.newFileUpload.fileName;
a.target = "_blank";

var fileImg = document.createElement('img');
fileImg.className = 'fileimg';

switch (data.newFileUpload.fileExtension) {
    case ".docx":
        fileImg.src = '/img/fileimg/wordimg.png';
        break;
    case ".xlsx":
        fileImg.src = '/img/fileimg/exelimg.png';
        break;
    case ".pptx":
        fileImg.src = '/img/fileimg/pwpimg.png';
        break;
    case ".pdf":
        fileImg.src = '/img/fileimg/pdfimg.png';
        break;
    default:
        fileImg.src = '/img/fileimg/other.png';
        break;
}

var h3 = document.createElement('h3');
h3.textContent = data.newFileUpload.fileName;

a.appendChild(fileImg);
fileDiv.appendChild(a);
fileDiv.appendChild(h3);


    var fileContainer = document.querySelector('.newFile');

    fileContainer.appendChild(fileDiv);

    var removeButtons = document.querySelectorAll('.corner-img');

removeButtons.forEach(function (button) {
    addRemoveHandler(button);
});
    })
});


function addRemoveHandler(button) {
    button.addEventListener('click', function () {
        var parentDiv = button.closest('.file');
        fetch("/Up/DeleteFile", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(button.id)
        })
            .then(response => response.json())
            .then(data => {
                if (data) {
                    if (parentDiv) {
                        parentDiv.remove();
                    }
                }
            });
    });
}

var removeButtons = document.querySelectorAll('.corner-img');

removeButtons.forEach(function (button) {
    addRemoveHandler(button);
});

