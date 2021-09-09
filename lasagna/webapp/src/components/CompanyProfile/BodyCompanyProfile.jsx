import React from 'react'
import { Checkbox, Container } from 'semantic-ui-react';
import CompanyTitleAndLink from './CompanyTitleAndLink';
import RuleOnegraph from './RuleOnegraph';


export default function BodyCompanyProfile() {
    
    //Return on investment
    //Earnings per share
    //Book value of equity per share 
    //Free cash flow
    
    //redux, cache?

    const data = [{year: '1990', ROIC: 1000, StickerPrice: 150, Score: 100, CashFlow: 1000}, 
              {year: '1991', ROIC: 200, StickerPrice: 410 , Score: 200, CashFlow: 200},
              {year: '1992', ROIC: -40, StickerPrice: 90, Score: 500, CashFlow: 2000},
              {year: '1993', ROIC: 80, StickerPrice: 620, Score: 100, CashFlow: 4000}];
              
    /* const graph= {
        if (ROIC == checked) {}
    } */

    return (
        <>
        <Container> 
            <CompanyTitleAndLink />           
        </Container>
                <Container className="ui fluid raised two column divided grid ">

                    <div id="checkboxes-list">

                        <div class="ui list">
                            <div class="item"><Checkbox label='ROIC' id='ROIC' /></div>
                            <div class="item"><Checkbox label='Sales growth rate' /></div>
                            <div class="item"><Checkbox label='EPS growth rate' /> </div>
                            <div class="item"><Checkbox label='BVPS growth rate' /> </div>
                            <div class="item"><Checkbox label='FCF growth rate' /></div>
                         </div>

                    </div> 
                        <RuleOnegraph />
                    <div>
  
                    </div>

                </Container>
    
        </>
    )
}