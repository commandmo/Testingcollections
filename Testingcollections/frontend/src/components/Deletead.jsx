import React from "react";
import Axios from "axios";
import { useEffect, useState } from "react";

function Deletead () {

  const [advertId, setAdvertId] = useState("");


  const deleteAdvert = () => {
  
    Axios.delete(`http://testingcollections-env.eba-xbwapgkd.ap-southeast-2.elasticbeanstalk.com/api/Advert/${advertId}`)
      .then((res) => {
        setAdvertId(res.data);
        console.log(res.data);
        const successAlert = alert("Advert has been deleted successfully")
      })
      .catch((res) => {
        const errorAlert = alert("Advert has already been deleted or does not exist")
      });

    };



  return (
    <div  className="note">
      <h1>Delete ad</h1>
      <input 
      placeholder="Advert ID" 
      onChange={(event) => {
        setAdvertId(event.target.value);
      }}
      />
      
      <button onClick={deleteAdvert}>Delete Advert</button>   

   
    </div>
  );
}
   

export default Deletead;
