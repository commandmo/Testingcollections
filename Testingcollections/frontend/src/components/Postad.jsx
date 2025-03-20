import React from "react";
import Axios from "axios";
import { useEffect, useState } from "react";

function Postad () {

    const [advert, setAdvert] = useState ({
        make: "",
        model: "",
        year: "",
        colour: ""
      })

    const handleInput = (event) => {
        setAdvert({advert, [event.target.name]: event.target.value})
    } 

    function handleSubmit(event) {
        event.preventDefault()
        Axios.post('http://testingcollections-env.eba-xbwapgkd.ap-southeast-2.elasticbeanstalk.com/api/Advert', {advert})
        .then(response => console.log(response))
        .catch(err => console.log(err))
         }

    

  return (
    <div  className="note">
      <h1>Create ad</h1>
      <form onSubmit={handleSubmit}>
        Make: <input type="text"onChange={handleInput} name="make"></input><br></br>
        Model: <input type="text"onChange={handleInput} name="model"></input><br></br>
        Year: <input type="text"onChange={handleInput} name="year"></input><br></br>
        Colour: <input type="text"onChange={handleInput} name="colour"></input><br></br>
        <button>Create ad</button>
      
      </form>

   
    </div>
  );
}
   

export default Postad;
