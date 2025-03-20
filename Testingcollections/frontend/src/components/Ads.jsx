import React from "react";
import Axios from "axios";
import { useEffect, useState } from "react";

function Ads() {

  const [advert, setAdvert] = useState("");

  const getAdvert = () => {
    Axios.get("http://testingcollections-env.eba-xbwapgkd.ap-southeast-2.elasticbeanstalk.com/api/Advert")
    .then((res) => {
      setAdvert(res.data);
      console.log(res.data);
    });
  };



  return (
    <div  className="note">
      <h1>Search all ads</h1>
      <button onClick={getAdvert}>Search Adverts</button>       
      <table> {advert && advert.map((ad) => <tr key={ad.id}>
        <tr>Advert ID</tr><td>{ad.id}</td>
        <tr>Year</tr><td>{ad.year}</td>
        <tr>Make</tr><td>{ad.make}</td>
        <tr>Model</tr><td>{ad.model}</td>
        <tr>Colour</tr><td>{ad.colour}</td>
        </tr>
      )}</table>
    </div>
  );
}
   

export default Ads;
