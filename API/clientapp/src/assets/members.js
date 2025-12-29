window.loadMembers = async function loadMembers() {
  console.log("Entry");

  if (!window.getMembers) {
    console.log("Waiting for Angular...");
    setTimeout(loadMembers, 100);
    return;
  }

  console.log("Calling API");
  const response = await window.getMembers();
  console.log(response);

  const listEl = document.getElementById('membersList');
  listEl.innerHTML = '';

  let htm = '';
  for(let member of response.data){
    
    htm += `<li>`;
    htm += `${member.name} (${member.email})`;
    htm += `</li>`;
   
  };
   listEl.innerHTML = htm;

   .add("hiden");
}
