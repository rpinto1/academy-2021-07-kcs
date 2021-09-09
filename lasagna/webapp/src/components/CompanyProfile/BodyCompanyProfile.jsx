import React, { useState } from 'react'
import { Radio, Container } from 'semantic-ui-react';
import CompanyTitleAndLink from './CompanyTitleAndLink';
import RuleOnegraph from './RuleOnegraph';


export default function BodyCompanyProfile() {
    
    //Return on investment
    // Sales growth rate
    //Earnings per share
    //Book value of equity per share 
    //Free cash flow
    
    const [aspect, setAspect] = useState({
        ROIC: false,
        SGR: false,
        EPS: false,
        BVPS: false, 
        FCF: false
    });
       
    const handleChange = (event) => {
        const { id } = event.target
        setAspect(prevState => ({
            ...prevState,
            [id]: !aspect     
        }))
    };


    return (
        <>
        <Container> 
            <CompanyTitleAndLink />           
        </Container>
                <Container className="ui fluid raised two column divided grid ">

                    <div id="checkboxes-list">

                        <div class="ui list">
                            <div class="item">
                                <Radio name='aspect'
                                label='ROIC' 
                                id='ROIC' 
                                onChange={handleChange}/>
                            </div>
                            <div class="item">
                                <Radio name='aspect'
                                label='Sales growth rate' 
                                id='SGR'
                                onChange= {handleChange}/>
                            </div>
                            <div class="item">
                                <Radio name='aspect'
                                label='EPS growth rate'
                                id='EPS'
                                onChange={handleChange}/>
                            </div>
                            <div class="item">
                                <Radio name='aspect'
                                label='BVPS growth rate' 
                                id= 'BVPS'
                                onChange={handleChange}/>
                            </div>
                            <div class="item">
                                <Radio name='aspect'
                                label='FCF' 
                                id='FCF'
                                onChange={handleChange}/>
                            </div>
                         </div>

                    </div> 
                        <RuleOnegraph aspect = {aspect}/>
                    <div>
  
                    </div>

                </Container>
    
        </>
    )
}