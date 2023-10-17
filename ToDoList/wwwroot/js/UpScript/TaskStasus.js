 
var task = document.querySelectorAll('input[type="checkbox"]');


task.forEach(button=>{
    button.addEventListener('click', function(){
        fetch("/Up/TaskStatus", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(button.id)
        })
            .then(response => response.json())
            .then(data => {
                if (data) {
                    
                }
            });
    })
});