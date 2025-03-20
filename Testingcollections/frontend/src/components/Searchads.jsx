import React from "react";
import Axios from "axios";
import { useEffect, useState } from "react";

function Searchads () {

  const [advertid, setAdvertid] = useState("");
  const [advertMake, setAdvertMake] = useState([]);
  const [advertModel, setAdvertModel] = useState([]);
  const [advertYear, setAdvertYear] = useState([]);
  const [advertColour, setAdvertColour] = useState([]);

  const getAdvert = () => {
  
    Axios.get(`http://testingcollections-env.eba-xbwapgkd.ap-southeast-2.elasticbeanstalk.com/api/Advert/${advertid}`)
      .then((res) => {
        setAdvertMake(res.data.make);
        setAdvertModel(res.data.model);
        setAdvertYear(res.data.year);
        setAdvertColour(res.data.colour);
        console.log(res.data);
      });
    };



  return (
    <div  className="note">
       <h1>Search ad</h1>
      <input 
      placeholder="Advert ID" 
      onChange={(event) => {
        setAdvertid(event.target.value);
      }}
      />
      <button onClick={getAdvert}>Search Adverts</button>   
      <table><tr><td>{advertYear}</td><td>{advertMake}</td><td>{advertModel}</td><td>{advertColour}</td></tr></table>

   
    </div>
  );
}
   

export default Searchads;
