const change = () => {
    const register = document.getElementById("register")
    register.style.visibility ="unset"


}

const register = async() =>{
   const user = {
     UserName : document.getElementById("userName").value,
     Password: document.getElementById("password").value,
     FirstName: document.getElementById("firstName").value,
     LastName: document.getElementById("lastName").value
}
    
    try {
    const progress = document.getElementById("progress").value;
    if (progress-1 < 2)
    throw new Error("Easy password,please change your password")
        const res = await fetch('api/Users',
            {
                method: 'POST',
                headers: {'Content-Type':'application/json' },
                body: JSON.stringify(user)

            })
        if (!res.ok)
            alert("error added to the server,please try again!")
        else 
        {
            const data = await res.json()
            alert(`user ${data.userName} registered succfully`)
        }
    }

    catch (e)
    { alert(e) }
}

const login = async () => {

    try {
        const UserName=document.getElementById("userName1").value
        const Password= document.getElementById("password1").value
        const res = await fetch(`api/Users?UserName=${UserName}&Password=${Password}`)
        if (res.status=='204')
            window.alert("userName or password are not valid")
        else { 
            const user=await res.json()
            sessionStorage.setItem("user", JSON.stringify(user))
            window.location.href="Update.html"
        }
    } catch (e) {
        alert(e)
    }



}
const update = async () => {
  
    const user = {
       
        UserName: document.getElementById("userNameToUpdate").value,
        Password: document.getElementById("passwordToUpdate").value,
        FirstName: document.getElementById("firstNameToUpdate").value,
        LastName: document.getElementById("lastNameToUpdate").value
    }
    try {
        const userJson = sessionStorage.getItem("user")
        const id = JSON.parse(userJson).userId
        const res = await fetch(`api/Users/${id}`,
        {
            method: 'PUT',
            headers: { 'Content-Type':`application/json` },
            body: JSON.stringify(user)
        })
        if (!res.ok)
            alert("error updated to the server,your password is too easy,change your password!")
        else {
           
            alert(`user ${id} updated succfully`)
        }


    } catch (e) {
      alert(e)
    }



}
async function strengthPassword() {
    const password = document.getElementById("password").value
    try {
        const res = await fetch("api/Users/password",
            {
                method: 'POST',
                headers: { 'Content-Type': `application/json` },
                body: JSON.stringify(password)

            })
        if (!res.ok)
            alert("this password not strength")
        const progress = document.getElementById("progress")
        const result = await res.json()
        progress.value = result+1

    } catch (e) {
        alert(e.massage)
    }




} 

const hellow = document.createElement('p')
document.body.appendChild(hellow)
const userJson = sessionStorage.getItem("user")
const firstName=JSON.parse(userJson).firstName
hellow.innerText =`wellcom to ${firstName}`