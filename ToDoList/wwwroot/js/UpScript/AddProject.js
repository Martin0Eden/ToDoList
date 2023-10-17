var buuton = document.querySelector('.add');

buuton.addEventListener('click',function(){
    var overlay = document.createElement('div');
overlay.className = 'overlay';

var formContainer = document.createElement('div');
formContainer.className = 'formcontainer';

var form = document.createElement('form');
form.method = 'post';
form.className = 'form';

var cardForm = document.createElement('div');
cardForm.className = 'card_form';

var loginLink = document.createElement('a');
loginLink.className = 'login';
loginLink.textContent = 'Добавить Проект';

var titleInputBox = document.createElement('div');
titleInputBox.className = 'inputBox';
var titleInput = document.createElement('input');
titleInput.type = 'text';
titleInput.required = true;
var titleLabel = document.createElement('span');
titleLabel.className = 'user';
titleLabel.textContent = 'Название';
titleInputBox.appendChild(titleInput);
titleInputBox.appendChild(titleLabel);

var descriptionInputBox = document.createElement('div');
descriptionInputBox.className = 'inputBox';
var descriptionInput = document.createElement('input');
descriptionInput.type = 'text';
descriptionInput.required = true;
var descriptionLabel = document.createElement('span');
descriptionLabel.textContent = 'Описание';
descriptionInputBox.appendChild(descriptionInput);
descriptionInputBox.appendChild(descriptionLabel);


cardForm.appendChild(loginLink);
cardForm.appendChild(titleInputBox);
cardForm.appendChild(descriptionInputBox);

form.appendChild(cardForm);
formContainer.appendChild(form);
overlay.appendChild(formContainer);

var addButton = document.createElement('a');
    addButton.textContent = 'Добавить';

    var exitLink = document.createElement('a');
    exitLink.textContent = 'Выход';
    cardForm.appendChild(addButton);
    cardForm.appendChild(exitLink);

document.body.appendChild(overlay);


    addButton.addEventListener('click',function(){
        var remove = document.querySelector('.overlay');
        remove.remove();
        var parent = document.querySelector('.task');

        var projectData = {
            ProjectId:parent.id,
            Name: titleInput.value,
            Description: descriptionInput.value,
        };


        fetch("/Up/AddProject", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(projectData)
        })
            .then(response => response.json())
            .then(data => {
                    var cardDiv = document.createElement('div');
                    cardDiv.className = 'card';

                    var infoDiv = document.createElement('div');
                    infoDiv.className = 'info';

                    var h2Element = document.createElement('h2');
                    h2Element.textContent = item.Name;
                    h2Element.className = 'cardText';

                    var pElement = document.createElement('p');
                    pElement.textContent = data.projects.description;

                    infoDiv.appendChild(h2Element);
                    infoDiv.appendChild(pElement);

                    var containerDiv = document.createElement('div');
                    containerDiv.className = 'container';

                    var containerOneDiv = document.createElement('div');
                    containerOneDiv.className = 'buttonsContainer containerOne';

                    var aButtonOne = document.createElement('a');
                    aButtonOne.setAttribute('asp-action', 'ProjectView');
                    aButtonOne.setAttribute('asp-controller', 'List');
                    aButtonOne.setAttribute('asp-route-id', data.projects.projectId);
                    aButtonOne.setAttribute('asp-route-userid', data.users.UserId);

                    var imgButtonOne = document.createElement('img');
                    imgButtonOne.src = '/img/pen.png';
                    imgButtonOne.className = 'buttons';

                    aButtonOne.appendChild(imgButtonOne);

                    containerOneDiv.appendChild(aButtonOne);

                    var containerTwoDiv = document.createElement('div');
                    containerTwoDiv.className = 'buttonsContainer containerTwo';

                    var aButtonTwo = document.createElement('a');
                    aButtonTwo.id = item.ProjectId;
                    aButtonTwo.className = 'deleteButton';


                    containerTwoDiv.appendChild(aButtonTwo);

                    containerDiv.appendChild(containerOneDiv);
                    containerDiv.appendChild(containerTwoDiv);

                    cardDiv.appendChild(infoDiv);
                    cardDiv.appendChild(containerDiv);

                    parent.appendChild(cardDiv);

                    
                
            })


    });

    exitLink.addEventListener('click',function(){
        var remove = document.querySelector('.overlay');
        remove.remove();
    });
});