var buuton = document.querySelectorAll('.AddTask');

buuton.forEach(buuton=>{
    buuton.addEventListener('click',function(){
        var overlay = document.createElement('div');
    overlay.className = 'overlay';
    
    var formContainer = document.createElement('div');
    formContainer.className = 'formcontainer';
    
    var form = document.createElement('form');
    form.className = 'form';
    
    var cardForm = document.createElement('div');
    cardForm.className = 'card_form';
    
    var loginLink = document.createElement('a');
    loginLink.className = 'login';
    loginLink.textContent = 'Добавить задачу';
    
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
    
    var addButton = document.createElement('button');
        addButton.className = 'enter';
        addButton.textContent = 'Добавить';
    
        var exitLink = document.createElement('a');
        exitLink.textContent = 'Выход';
        cardForm.appendChild(addButton);
        cardForm.appendChild(exitLink);
    
    document.body.appendChild(overlay);
    
        addButton.addEventListener('click',function(){
            var id = buuton.closest('.sprintlist');
    
            var tasktData = {
                SprintId:id.id,
                Name: titleInput.value,
                Description: descriptionInput.value,
            };
    
    
            fetch("/Up/AddTask", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(tasktData)
            })
                .then(response => response.json())
                .then(data => {
                    if(data!=null)
                    {
                    var taskDiv = document.createElement('div');
                    taskDiv.className = 'task';

                    var userid = document.querySelector('.comment');

                    var taskLink = document.createElement('a');
                    taskLink.setAttribute('asp-action', 'Task');
                    taskLink.setAttribute('asp-controller', 'List');
                    taskLink.setAttribute('asp-route-iduser', userid.id);
                    taskLink.setAttribute('asp-route-idtask', data.taskId);




                    var taskInfoDiv = document.createElement('div');
                    taskInfoDiv.className = 'taskinfo';


                    var h3 = document.createElement('h3');
                    h3.textContent = task.Name;

                    var p = document.createElement('p');
                    p.textContent = data.description;

                    taskInfoDiv.appendChild(h3);
                    taskInfoDiv.appendChild(p);

                    taskLink.appendChild(taskInfoDiv);


                    taskDiv.appendChild(taskLink);


                    var label = document.createElement('label');
                    label.className = 'container';


                    var input = document.createElement('input');
                    input.id = task.TaskId;
                    input.type = 'checkbox';


                    var checkmarkDiv = document.createElement('div');
                    checkmarkDiv.className = 'checkmark';


                    label.appendChild(input);
                    label.appendChild(checkmarkDiv);

                    taskDiv.appendChild(label);

                    var tasklist= id.querySelector('.tasklist');
                    tasklist.appendChild(taskDiv);
                    }
                })
    
    
        });
    
        exitLink.addEventListener('click',function(){
            var remove = document.querySelector('.overlay');
            remove.remove();
        });
    });
});

