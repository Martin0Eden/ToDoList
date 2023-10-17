var button = document.querySelector('.role');

button.addEventListener('click',function(){

    formloaded(); 

});


function formloaded()
{
    var overlay = document.createElement('div');
    overlay.className = 'overlay';

    var upuser = document.createElement('div');
    upuser.id = 'generatedId'; 
    upuser.className = 'upuser';

    var h2 = document.createElement('h2');
    h2.textContent = ' Пользователи';

    var scrollbar = document.createElement('div');
    scrollbar.className = 'scrollbar';

    var adduserbtn = document.createElement('a');
    adduserbtn.id = 'adduserbtn';
    adduserbtn.href = '#';
    adduserbtn.className = 'btn';
    adduserbtn.textContent = 'Выход';



    fetch("/Up/Users", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(true)
    })
        .then(response => response.json())
        .then(data => {
            
        data.forEach(element => {
        
        var newElement = document.createElement('div');
        newElement.setAttribute('data-taskid', element.userId);
        newElement.className = 'UsersUp';

        var you = document.querySelector('.task');

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
        var con = document.createElement('div');
        if(element.userId!==you.id)
        {
            if(role!=="Admin")
            {
                var arrowRight = document.createElement('img');
                arrowRight.src = '/img/arrow.png'; 
                arrowRight.className = 'arrow-right';
                
                con.appendChild(arrowRight);    
            }
    
           if(role!=="User")
           {
            var arrowLeft = document.createElement('img');
            arrowLeft.src = '/img/arrow.png'; 
            arrowLeft.className = 'arrow-left';
            
            con.appendChild(arrowLeft);
           }
        }
       

newElement.appendChild(h3);
newElement.appendChild(con);
        scrollbar.appendChild(newElement);
    }); 
    upuser.appendChild(h2);
    upuser.appendChild(scrollbar);
    upuser.appendChild(adduserbtn);

    overlay.appendChild(upuser);

    document.body.appendChild(overlay);


    var upbtn = document.querySelectorAll('.arrow-right');
    upbtn.forEach(element=>{
        element.addEventListener('click',function(){
             var upuser = element.closest('.UsersUp');
             var iduser= upuser.getAttribute('data-taskid');
             console.log(iduser);
             fetch("/Up/UpRole", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(iduser)
            })
            .then(response => response.json())
            .then(data => {
                if(data)
                {
                    overlay.remove();
                    formloaded(); 
                }
            })
        });
    });

    var downbtn = document.querySelectorAll('.arrow-left');
    downbtn.forEach(element=>{
        element.addEventListener('click',function(){
             var upuser = element.closest('.UsersUp');
             var iduser= upuser.getAttribute('data-taskid');
             console.log(iduser);
             fetch("/Up/DownRole", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(iduser)
            })
            .then(response => response.json())
            .then(data => {
                if(data)
                {
                    overlay.remove();
                    formloaded(); 
                }
            })
        });
    });


    adduserbtn.addEventListener('click', function(){
        overlay.remove();
    });
})
}