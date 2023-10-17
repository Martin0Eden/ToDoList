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
loginLink.textContent = 'Добавить Спринт';

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

var startInputBox = document.createElement('div');
startInputBox.className = 'inputBox';
var startInput = document.createElement('input');
startInput.type = 'text';
startInput.required = true;
var startLabel = document.createElement('span');
startLabel.textContent = 'Начало';
startInputBox.appendChild(startInput);
startInputBox.appendChild(startLabel);

var endInputBox = document.createElement('div');
endInputBox.className = 'inputBox';
var endInput = document.createElement('input');
endInput.type = 'text';
endInput.required = true;
var endLabel = document.createElement('span');
endLabel.textContent = 'Окончание';
endInputBox.appendChild(endInput);
endInputBox.appendChild(endLabel);

cardForm.appendChild(loginLink);
cardForm.appendChild(titleInputBox);
cardForm.appendChild(descriptionInputBox);
cardForm.appendChild(startInputBox);
cardForm.appendChild(endInputBox);

form.appendChild(cardForm);
formContainer.appendChild(form);
overlay.appendChild(formContainer);

var addButton = document.createElement('button');
    addButton.className = 'enter';
    addButton.textContent = 'Добавить';

    var exitLink = document.createElement('a');
    exitLink.textContent = 'Выход';
    cardForm.appendChild(addButton);
    cardForm.appendChild(exitLink);

document.body.appendChild(overlay);

    addButton.addEventListener('click',function(){
        var id = document.querySelector('.info');

        var sprintData = {
            ProjectId:id.id,
            Name: titleInput.value,
            Description: descriptionInput.value,
            StartDate: new Date(startInput.value).toISOString(),
            EndDate: new Date(endInput.value).toISOString()
        };


        fetch("/Up/AddSprint", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(sprintData)
        })
            .then(response => response.json())
            .then(data => {
                if(data!=null)
                {
                var sprintListDiv = document.createElement('div');
                sprintListDiv.className = 'sprintlist';
                
                var headDiv = document.createElement('div');
                headDiv.className = 'head';
                
                var headProjectDiv = document.createElement('div');
                headProjectDiv.className = 'headProject';
                
                var projectNameH2 = document.createElement('h2');
                projectNameH2.textContent = data.name;
                
                var dateInfoH3 = document.createElement('h3');

                var startString = data.startDate.ToString("dd.MM HH:mm");
                var endString = data.endDate.ToString("dd.MM HH:mm");

                dateInfoH3.textContent = "Начало: "+startString+ "Конец: "+endString;
                
                var descriptionP = document.createElement('p');
                descriptionP.textContent = data.description;
                
                headProjectDiv.appendChild(projectNameH2);
                headProjectDiv.appendChild(dateInfoH3);
                headProjectDiv.appendChild(descriptionP);
                
                var addTaskImg = document.createElement('img');
                addTaskImg.src = '/img/Add.png';
                addTaskImg.className = 'AddTask';
                
                headDiv.appendChild(headProjectDiv);
                headDiv.appendChild(addTaskImg);
                
                var taskListDiv = document.createElement('div');
                taskListDiv.className = 'tasklist';
                
                sprintListDiv.appendChild(headDiv);
                sprintListDiv.appendChild(taskListDiv);
                id.appendChild(sprintListDiv);
                }
            })


    });

    exitLink.addEventListener('click',function(){
        var remove = document.querySelector('.overlay');
        remove.remove();
    });
});