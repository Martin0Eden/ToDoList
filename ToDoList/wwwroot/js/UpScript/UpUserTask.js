document.getElementById('Adbtn').addEventListener('click', function () {

    var overlay = document.createElement('div');
    overlay.className = 'overlay';

    var upuser = document.createElement('div');
    upuser.id = 'generatedId'; 
    upuser.className = 'upuser';

    var h2 = document.createElement('h2');
    h2.textContent = 'Добавить пользователя';

    var scrollbar = document.createElement('div');
    scrollbar.className = 'scrollbar';

    var adduserbtn = document.createElement('a');
    adduserbtn.id = 'adduserbtn';
    adduserbtn.href = '#';
    adduserbtn.className = 'btn';
    adduserbtn.textContent = 'Добавить';

    var taskid= document.querySelector('.info');

    fetch("/Up/UserNotTask", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(taskid.id)
    })
        .then(response => response.json())
        .then(data => {
            
        data.forEach(element => {
        
        var newElement = document.createElement('div');
        newElement.setAttribute('data-taskid', element.userId);
        newElement.className = 'UsersUp';

        var h3 = document.createElement('h3');
        var role;
        switch(element.role){
            case "R1":
                role="Admin";
                break;
            case "R2":
                role = "Menager";
                break;
            case "R3":
                role = "User";
                break;
                
        }
        h3.textContent = role+': '+element.name;

        newElement.appendChild(h3);
        scrollbar.appendChild(newElement);
    }); 
    upuser.appendChild(h2);
    upuser.appendChild(scrollbar);
    upuser.appendChild(adduserbtn);

    overlay.appendChild(upuser);

    document.body.appendChild(overlay);


    var task = document.querySelector('.info');
    var divs = document.querySelectorAll('.UsersUp');
    var btn = document.getElementById('adduserbtn');
    var selectedIds = [];
    
    divs.forEach(function (div) {
        div.addEventListener('click', function () {
            var taskId = div.getAttribute('data-taskid');
    
            if (div.style.backgroundColor !== 'rgb(94, 219, 83)') {
                div.style.backgroundColor = '#5edb53';
                selectedIds.push(taskId);
            } else {
                div.style.backgroundColor = 'rgb(169, 169, 169)';
                var index = selectedIds.indexOf(taskId);
                if (index !== -1) {
                    selectedIds.splice(index, 1);
                }
            }
        });
    });
    
    btn.addEventListener('click', function () {
        var userTaskArray = selectedIds.map(function (userId) {
            return {
                UserTaskId: null,
                UserId: userId,
                TaskId: task.id
            };
        });
    
    
        fetch("/Up/UserAddTask", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(userTaskArray)
        })
        .then(response => response.json())
        .then(data => {
            if (data) {
                var overlay = document.querySelector('.overlay');
                overlay.remove();

                fetch("/Up/UserAddTaskView", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(userTaskArray)
                })
                .then(response => response.json())
                .then(data => {
                    var parent = document.querySelector('.userscroll');
                    data.forEach(element=>{

                        var newDiv = document.createElement('div');
                        newDiv.setAttribute('data-taskid', task.id);
                        newDiv.className = 'Users';


                        var h3 = document.createElement('h3');
                        var role;
                        switch(element.role){
                            case "R1":
                                role="Admin";
                                break;
                            case "R2":
                                role = "Menager";
                                break;
                            case "R3":
                                role = "User";
                                break;
                                
                        }
                        h3.textContent = role + ': ' + element.name;


                    var img = document.createElement('img');
                    img.id = element.userId;
                    img.src = '/img/remove.png';
                    img.className = 'remove';


                    newDiv.appendChild(h3);
                    newDiv.appendChild(img);

                    parent.appendChild(newDiv);
                    });
                    var removeButtons = document.querySelectorAll('.remove');

                    removeButtons.forEach(function (button) {
                        addRemoveHandler(button);
                    });
                });

            }
        });
    });
});
});

function addRemoveHandler(button) {
    button.addEventListener('click', function () {
        var parentDiv = button.closest('.Users');
        var userId = button.id;

        var taskid = document.querySelector('.info');
        console.log(taskid.id);
        var dataid = {
            UserId: userId,
            TaskId: taskid.id
        };
        fetch("/Up/DeleteUserTask", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dataid)
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

var removeButtons = document.querySelectorAll('.remove');

removeButtons.forEach(function (button) {
    addRemoveHandler(button);
});
