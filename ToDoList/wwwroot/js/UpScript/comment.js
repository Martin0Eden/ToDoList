var comment = document.querySelector('.comment');

comment.addEventListener('click', function(){
    var id = document.querySelector('.info');
   
var overlay = document.createElement('div');
overlay.className = 'overlay';

var card = document.createElement('div');
card.className = 'card';

var cornerImg = document.createElement('img');
cornerImg.className = 'corner-img';
cornerImg.src = '/img/remove.png'; 

var chatHeader = document.createElement('div');
chatHeader.className = 'chat-header';
chatHeader.textContent = 'Chat';

var chatWindow = document.createElement('div');
chatWindow.className = 'chat-window';



var chatInput = document.createElement('div');
chatInput.className = 'chat-input';

var messageInput = document.createElement('input');
messageInput.type = 'text';
messageInput.className = 'message-input';
messageInput.placeholder = 'Текст сообщения';

var sendButton = document.createElement('button');
sendButton.className = 'send-button';
sendButton.textContent = 'Send';

chatInput.appendChild(messageInput);
chatInput.appendChild(sendButton);

card.appendChild(cornerImg);
card.appendChild(chatHeader);
card.appendChild(chatWindow);
 fetch("/Up/Allcoment", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(id.id)
            })
                .then(response => response.json())
                .then(data => {
                    if(data)
                    {
                        messageInput.value='';
                        var i =0;
                        data.comments.forEach(element => {
                            var messageList = document.createElement('div');
                            var h4 = document.createElement('h4');
                            h4.textContent = data.user[i].name;
                            h5=document.createElement('h5');
                            h5.textContent = element.text;
                            messageList.appendChild(h4);
                            messageList.appendChild(h5);
                            messageList.className = 'message-list';
                            chatWindow.appendChild(messageList);
                            i++;
                        });

                    }
                })



card.appendChild(chatInput);

overlay.appendChild(card);

document.body.appendChild(overlay);



    sendButton.addEventListener('click',function(){
        if(messageInput.value!=='')
        { 
            var comment = document.querySelector('.comment');
            var idIn = id.id;
            var commentId = comment.id;
            var text = messageInput.value;
            
            var data = {
                IdIn: idIn,
                UserId:commentId,
                Text: text
            };
            console.log(data);
            fetch("/Up/AddComment", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            })
                .then(response => response.json())
                .then(data => {
                    if(data!=null)
                    {
                        messageInput.value='';
                        var messageList = document.createElement('div');
                            var h4 = document.createElement('h4');
                            h4.textContent = data.users.name;
                            h5=document.createElement('h5');
                            h5.textContent = data.comment.text;
                            messageList.appendChild(h4);
                            messageList.appendChild(h5);
                            messageList.className = 'message-list';
                            chatWindow.appendChild(messageList);
                    }
                })
        }
    });
    cornerImg.addEventListener('click',function(){
        var remove = document.querySelector('.overlay');
        remove.remove();

});

});