const showRegistrationForm =()=> {
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
        if (progress - 1 < 2)
            alert("Easy password,please change your password")
        else { 
        const res = await fetch('api/Users',
            {
                method: 'POST',
                headers: {'Content-Type':'application/json' },
                body: JSON.stringify(user)

            })
        if (!res.ok)
            alert("Registration failed, please try again!!")
        else 
        {
            const data = await res.json()
            alert(`user ${data.firstName} registered successfully`)
            }
        }
    }

    catch (e)
    { alert(e.massage) }
}

const login = async () => {

    try {
        const UserNameAndPassword = {
            UserName:document.getElementById("userName1").value,
            Password: document.getElementById("password1").value
        }
        const res = await fetch('api/Users/login',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(UserNameAndPassword)
            })
        if (!res.ok ||res.status=='401')
            window.alert("userName or password are not valid")
        else { 
            const user=await res.json()
            sessionStorage.setItem("user", JSON.stringify(user))
            window.location.href="/SuperMarket.html"
        }
    } catch (e) {
        alert(e)
    }



}
const update = async () => {
  
    const user = {
       
        UserName: document.getElementById("userNameToUpdate").value,
        Password: document.getElementById("password").value,
        FirstName: document.getElementById("firstNameToUpdate").value,
        LastName: document.getElementById("lastNameToUpdate").value
    }
    try {
        const userJson = sessionStorage.getItem("user")
        const id = JSON.parse(userJson).userId
        const progress = document.getElementById("progress").value;
        if (progress - 1 < 2)
            alert("Easy password,please change your password")
        else { 
        const res = await fetch(`api/Users/${id}`,
        {
            method: 'PUT',
            headers: { 'Content-Type':`application/json` },
            body: JSON.stringify(user)
        })
        if (!res.ok)
            alert("error updated to the server,your password is too easy,change your password!")
        else {
            sessionStorage.user = JSON.stringify(await res.json())
            const userJson = sessionStorage.getItem("user")
            const firstName = JSON.parse(userJson).firstName 
            document.getElementById("hello").innerText = `welcome to ${firstName}`
            alert(`user ${id} updated succfully`)
        }
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
        const progress = document.getElementById("progress")
        if (!res.ok) {
            alert("The password is not strong or Weak password")
            progress.value = 1
        }
        else {
            const result = await res.json()
            progress.value = result+1 
        }

    } catch (e) {
        alert(e.massage)
    }




} 
const welcome = () => { 
    const hello = document.createElement('p')
hello.id="hello"
document.body.appendChild(hello)
const userJson = sessionStorage.getItem("user")
const firstName=JSON.parse(userJson).firstName
    hello.innerText = `welcome to ${firstName}`
}