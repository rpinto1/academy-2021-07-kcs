import React, { useEffect } from 'react';
import { loadCaptchaEnginge, LoadCanvasTemplate } from 'react-simple-captcha';


export default function Captcha() {


    useEffect(() => {
        loadCaptchaEnginge(6)
    }, []);

       return (

           <div className="container">
               <div className="form-group">
                   <div></div>
                   <div className="col mt-3">
                       <LoadCanvasTemplate />
                   </div>             

               </div>

           </div>


       );

   };