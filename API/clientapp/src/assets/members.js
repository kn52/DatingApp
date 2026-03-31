console.log('members.js loaded');  // top of file
console.log('document.readyState', document.readyState);

(function () {

  function waitForAngular() {
    if (!window.getMembers) {
      setTimeout(waitForAngular, 50);
      return;
    }
    loadMembers();
  }

  async function loadMembers() {
    console.log('DOM + Angular ready');

    const response = await window.getMembers();

    const listEl = document.getElementById('membersList');
    if (!listEl) return;

    listEl.innerHTML = `
      <table class="table w-full">
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          ${response.data.map((m, index) => `
            <tr>
              <td>${index + 1}</td>
              <td>${m.name}</td>
              <td>
                <button class="btn btn-primary" onclick="loadMemberById('${m.id}')">Load</button>
              </td>
            </tr>
          `).join('')}
        </tbody>
      </table>
    `;
    document.getElementById('loading').classList.add("hidden");
  }

  // DOM Ready
  if (document.readyState === 'interactive') {
    loadMembers();
  }

  window.loadMemberById = async function (id) {
    console.log('loadMemberById', id);
    if (!id) return;

    const response = await window.getMemberById(id);
    console.log('getMemberById response', response);

    if (response.success && response.data) {
      this.member = response.data
    
      const listEl = document.getElementById('memberEditById');
      if (!listEl) return;

      listEl.innerHTML = `
      <p>Member Details:</p>
      <table class="table w-full">
        <thead>
          <tr>
            <th>Display</th>
            <th>Value</th>
          </tr>
        </thead>
        <tbody>
            ${
                this.member
                  ? `
                    <tr>
                      <td>Id</td>
                      <td>${this.member.id}</td>
                    </tr>
                    <tr>
                      <td>Name</td>
                      <td>${this.member.name}</td>
                    </tr>
                    <tr>
                      <td>Email</td>
                      <td>${this.member.email}</td>
                    </tr>
                  `
                  : `
                    <tr>
                      <td colspan="2" style="text-align:center;">
                        No data found
                      </td>
                    </tr>
                  `
              }
        </tbody>
      </table>
      <button class="btn btn-primary" onclick="updateMemberById('${this.member.id}')">Upload</button>
      <button class="btn btn-primary" onclick="deleteMemberById('${this.member.id}')">Delete</button>`;


    } else {
      this.member = null;
      console.error(response.message);
    }
  }

  window.addMemberById = async function () {
    console.log('addMemberById');

    const listEl = document.getElementById('memberEditById');
    if (!listEl) return;

    listEl.innerHTML = `
      <p>Member Details:</p>
      <table class="table w-full">
        <thead>
          <tr>
            <th>Display</th>
            <th>Value</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Id</td>
            <td><input id="aid" placeholder="Enter id" /></td>
          </tr>
          <tr>
            <td>Name</td>
            <td><input id="aname" placeholder="Enter name" /></td>
          </tr>
          <tr>
            <td>Email</td>
            <td><input id="aemail" placeholder="Enter email" /></td>
          </tr>
        </tbody>
      </table>
      <button class="btn btn-primary" onclick="no_add_Member()">Cancel</button>
      <button class="btn btn-primary" onclick="add_Member()">Add</button>`;
  }

  window.no_add_Member = function () {
    console.log('no_add_Member');
    document.getElementById('memberEditById').innerHTML = '';
  }

  window.add_Member = async function () {
    console.log('add_Member');
    const addMember = {
      id: document.getElementById('aid').value, 
      name: document.getElementById('aname').value,
      email: document.getElementById('aemail').value         
    };

    const response = await window.addMember(addMember);

    if (response.success) {
      alert('Member added successfully');
      document.getElementById('memberEditById').innerHTML = '';
      loadMembers();
    }
    else {
      alert(response.message);
    }
  }

  window.updateMemberById = async function (id) {
    console.log('updateMemberById', id);
    if (!id) return;

    const response = await window.getMemberById(id);
    console.log('getMemberById response', response);

    if (response.success && response.data) {
      this.member = response.data
    
      const listEl = document.getElementById('memberEditById');
      if (!listEl) return;

      listEl.innerHTML = `
      <p>Member Details:</p>
      <table class="table w-full">
        <thead>
          <tr>
            <th>Display</th>
            <th>Value</th>
          </tr>
        </thead>
        <tbody>
            ${
                this.member
                  ? `
                    <tr>
                      <td>Id</td>
                      <td><input id="uid" value="${this.member.id}" /></td>
                    </tr>
                    <tr>
                      <td>Name</td>
                      <td><input id="uname" value="${this.member.name}" /></td>
                    </tr>
                    <tr>
                      <td>Email</td>
                      <td><input id="uemail" value="${this.member.email}" /></td>
                    </tr>
                  `
                  : `
                    <tr>
                      <td colspan="2" style="text-align:center;">
                        No data found
                      </td>
                    </tr>
                  `
              }
        </tbody>
      </table>
      <button class="btn btn-primary" onclick="no_update_Member()">Cancel</button>
      <button class="btn btn-primary" onclick="update_Member('${this.member.id}')">Upload</button>`;
    } else {
      this.member = null;
      console.error(response.message);
    }
  }

  window.no_update_Member = function () {
    console.log('no_update_Member');
    document.getElementById('memberEditById').innerHTML = '';
  }

  window.update_Member = async function (id) {
    console.log('update_Member', id);
    const updatedMember = {
      id: document.getElementById('uid').value, 
      name: document.getElementById('uname').value,
      email: document.getElementById('uemail').value         
    };

    const response = await window.updateMember(updatedMember);

    if (response.success) {
      alert('Member updated successfully');
      document.getElementById('memberEditById').innerHTML = '';
      loadMembers();
    }
    else {
      alert(response.message);
    }
  }

  window.deleteMemberById = async function (id) {
    console.log('deleteMemberById', id);
    if (!id) return;

    const response = await window.getMemberById(id);
    console.log('getMemberById response', response);

    if (response.success && response.data) {
      this.member = response.data
    
      const listEl = document.getElementById('memberEditById');
      if (!listEl) return;

      listEl.innerHTML = `
      <p>Member Details:</p>
      <table class="table w-full">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
          </tr>
        </thead>
        <tbody>
            ${
                this.member
                  ? `
                    <tr>
                      <td>${this.member.id}</td>
                      <td>${this.member.name}</td>
                      <td>${this.member.email}</td>
                    </tr>
                  `
                  : `
                    <tr>
                      <td colspan="3" style="text-align:center;">
                        No data found
                      </td>
                    </tr>
                  `
              }
        </tbody>
      </table>
      <p>Are you sure you want to delete this member?</p>
      <button class="btn btn-primary" onclick="no_delete_Member()">Cancel</button>
      <button class="btn btn-primary" onclick="delete_Member('${this.member.id}')">Delete</button>`;


    } else {
      this.member = null;
      console.error(response.message);
    }
  }

  window.no_delete_Member = function () {
    console.log('no_delete_Member');
    document.getElementById('memberEditById').innerHTML = '';
  }

  window.delete_Member = async function (id) {
    console.log('delete_Member', id);

    const response = await window.deleteMember(id);

    if (response.success) {
      alert('Member deleted successfully');
      document.getElementById('memberEditById').innerHTML = '';
      loadMembers();
    }
    else {
      alert(response.message);
    }
  }

})();
