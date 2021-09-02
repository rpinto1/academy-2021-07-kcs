import React from 'react';
import './App.css';
import 'semantic-ui-css/semantic.min.css';
import { Route , Switch } from 'react-router-dom';
import AboutUsView from './Views/AboutUsView';
import HomepageGuest from './Views/HomepageGuest';
import SignInView from './Views/SignInView';
import SignUpView from './Views/SignUpView';
import ContactUsView from './Views/ContactUsView';
import CompanyProfileView from './Views/CompanyProfileView';
import ForgottenPasswordView from './Views/ForgottenPasswordView';
import UserProfileView from './Views/UserProfileView';



function App() {

    return (
             
        
                
        <div>
         
            
            <Switch>
                <Route exact path ='/' component={HomepageGuest} />  
                <Route path ='/signup' component = {SignUpView} /> 
                <Route path ='/signin' component ={SignInView} />
                <Route path ='/contactus' component={ContactUsView} />
                <Route path ='/aboutus' component ={AboutUsView} />
                <Route path= '/company/details/${ticker}' component={CompanyProfileView} />
                <Route path='/forgottenpassword' component={ForgottenPasswordView} />
                <Route path= '/user/profile' component = {UserProfileView} />
            </Switch> 
           
        
           


        </div>
      
    )

}

export default App;
