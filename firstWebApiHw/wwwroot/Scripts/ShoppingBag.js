showBag = () => {
let cartString=sessionStorage.getItem("cart")
    let myCart = JSON.parse(cartString);
    let totalSum = 0;
    let i = 0;
    while (myCart.length != 0) {
        let c = myCart[0];
        let tmp = document.getElementById("temp-row");
        let clone = tmp.content.cloneNode(true);
        let count = myCart.filter(p => p.productId == myCart[i].productId)
        clone.querySelector(".image").src = "images/" + myCart[i].productImage;
        clone.querySelector(".quantity").innerHTML = count.length;
        clone.querySelector(".DeleteButton").addEventListener('click', () => deleteProduct(c))
        clone.querySelector(".AddButton").addEventListener('click', () => addProduct(c))
        clone.querySelector(".sum").innerHTML = count.length * myCart[i].productPrice+"$";
        totalSum += count.length * myCart[i].productPrice;
        document.querySelector("tbody").appendChild(clone);
        myCart = myCart.filter(p => p.productId != myCart[i].productId)
    }
    document.querySelector("#totalAmount").innerHTML=totalSum+"$"
}
deleteProduct=(product)=>{
    let cartString = sessionStorage.getItem("cart")
    let myCart = JSON.parse(cartString);
    let index = myCart.findIndex(p => p.productId == product.productId);
    myCart = myCart.filter((p, i) => i != index);
    sessionStorage.cart = JSON.stringify(myCart);
    document.querySelector("tbody").replaceChildren([]);
    showBag();
}
addProduct = (product) => {
    let cartString = sessionStorage.getItem("cart")
    let myCart = JSON.parse(cartString);
    myCart = [...myCart,product];
    sessionStorage.cart = JSON.stringify(myCart);
    document.querySelector("tbody").replaceChildren([]);
    showBag();
}

closeOrder = async() => {
    if (!sessionStorage.getItem("user")) {
        document.querySelector(".setUser").href = "/Login.html"
    }
    else {

        const order = {
            
            OrderDate: new Date(),
            OrderSum: 0,
            UserId: (JSON.parse(sessionStorage.getItem("user"))).userId,
            OrderItems: []
        }
        
        let cartString = sessionStorage.getItem("cart")
        let myCart = JSON.parse(cartString);
        let totalSum=0;
        while (myCart.length != 0) {
            let item = myCart[0];
            let count = myCart.filter(p => p.productId == item.productId).length
            let orderItem = {
                ProductId: item.productId,
                Quantity: count,
            }
            order.OrderItems = [...order.OrderItems, orderItem]
            totalSum += count * item.productPrice;
            myCart = myCart.filter(p => p.productId != item.productId)
        }
        order.OrderSum = totalSum;
        console.log(order)
        try {
            const res = await fetch(`https://localhost:44354/api/Orders`,
                    {
                        method: 'POST',
                        headers: { 'Content-Type': `application/json` },
                        body: JSON.stringify(order)
                    })
            if (res.status != '201')
                    alert("error create this order,plase try again!")
                else {
                    const data = await res.json();
                    alert(`order ${data.orderId} created succfully`)
                }
            

        } catch (e) {
            alert(e)//you need to write specific eror!
            //you can say error!
        }
    }


}
    




    
   




