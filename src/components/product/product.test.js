import React from 'react';
import { shallow } from 'enzyme';
import Product from './product';

describe('<Product />', () => {
  test('renders', () => {
    const wrapper = shallow(<Product />);
    expect(wrapper).toMatchSnapshot();
  });
});
