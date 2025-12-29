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

    listEl.innerHTML = response.data
      .map(m => `<li class="list-row items-center">
      <img src="/user.png" alt="" class="size-12 rounded-box"/>
      <div>
          ${m.name} (${m.email})
      </div>
      </li>`)
      .join('');

    document.getElementById('loading').classList.add("hidden");
  }

  // DOM Ready
  if (document.readyState === 'interactive') {
    loadMembers();
  }


})();
