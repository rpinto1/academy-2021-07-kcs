import React, { useEffect } from 'react';

import { loadCaptchaEnginge, LoadCanvasTemplate } from 'react-simple-captcha';


export default function Captcha() {


    useEffect(() => {
        loadCaptchaEnginge(6)
    }, []);

       return (

           <div className="container">
               <div className="form-group">
                   <div className="col mt-3">
                       <LoadCanvasTemplate />
                   </div>

                   <div className="col mt-3">
                       <div>
                           <input 
                           placeholder="Enter Captcha Value" 
                           id="user_captcha_input" 
                           name="user_captcha_input" 
                           type="text">
                            </input>
                        </div>

                   </div>                

               </div>

           </div>


       );

   };