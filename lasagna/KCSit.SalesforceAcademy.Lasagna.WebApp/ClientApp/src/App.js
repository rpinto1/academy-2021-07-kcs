import React from 'react';
import './App.css';
import 'semantic-ui-css/semantic.min.css';
import HomepageGuest from './Views/HomepageGuest';
import { Route } from 'react-router-dom';



function App() {
    <>
    <Route exact path ='/' component={HomepageGuest} />  
   
    </>

    return (
             
        
                
        <div>

            


            <HomepageGuest />


           


        </div>
      
    )

}

export default App;
