import { Box } from '@mui/material'
import { FC, memo } from 'react'
import AddProcessButton from './AddProcessButton/AddProcessButton'
import classNames from 'classnames'
import DataTable from '../../DataTable/DataTable'
import FullScreenDialogButton from '../../FullScreenDialogButton/FullScreenDialogButton'
import styles from './Buttons.module.scss'

interface ButtonsProps {
	page: 'release' | 'approval'
}

const Buttons: FC<ButtonsProps> = memo(({ page }) => {
	return (
		<Box
			className={classNames(styles.container, {
				'self-end': page === 'approval',
			})}
		>
			{page === 'release' && <AddProcessButton />}
			<FullScreenDialogButton title='Tабличное представление' icon='table'>
				<DataTable />
			</FullScreenDialogButton>
		</Box>
	)
})

export default Buttons
