import React from 'react';
import './App.css';
import 'semantic-ui-css/semantic.min.css';
import { Route , Switch } from 'react-router-dom';
import HomepageView from './Views/HomepageView';
import SignInView from './Views/SignInView';
import AdminView from './Views/AdminView';


function App() {

    return (
             
        <div>
                     
            <Switch>
                <Route exact path ='/' component={HomepageView} />  
                <Route path ='/signin' component ={SignInView} />
                <Route path= '/admin' component = {AdminView} /> 
            </Switch>  

        </div>
      
    )

}

export default App;
